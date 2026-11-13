using Neo4j.Driver;
using Neo4jApp.Models;

namespace Neo4jApp.Services;

public class Neo4jService(IDriver driver, IConfiguration configuration)
{
    private static string NormalizeCity(string name)
    {
        var normalized = name.Trim().ToLowerInvariant();
        return normalized == "vienna" ? "wien" : normalized;
    }

    public async Task<CityRoute?> FindRouteAsync(string origin, string destination)
    {
        var from = NormalizeCity(origin);
        var to = NormalizeCity(destination);
        await using var session = driver.AsyncSession(options =>
            options.WithDatabase(configuration["Neo4j:Database"]!));

        return await session.ExecuteReadAsync(async transaction =>
        {
            // The classroom example treats the connections as traversable in both directions.
            var query = from == to
                ? """
                  MATCH (city:City)
                  WHERE CASE toLower(city.name) WHEN 'vienna' THEN 'wien' ELSE toLower(city.name) END = $from
                  RETURN [city.name] AS cities, [] AS legs LIMIT 1
                  """
                : """
                  MATCH (start:City), (destination:City)
                  WHERE CASE toLower(start.name) WHEN 'vienna' THEN 'wien' ELSE toLower(start.name) END = $from
                    AND CASE toLower(destination.name) WHEN 'vienna' THEN 'wien' ELSE toLower(destination.name) END = $to
                  MATCH path = shortestPath((start)-[:DISTANCE*]-(destination))
                  RETURN [city IN nodes(path) | city.name] AS cities,
                         [edge IN relationships(path) | {distance: edge.value, unit: coalesce(edge.unit, '')}] AS legs
                  ORDER BY length(path) LIMIT 1
                  """;
            var cursor = await transaction.RunAsync(query, new { from, to });
            var routes = await cursor.ToListAsync(record => new CityRoute(
                record["cities"].As<List<string>>(),
                record["legs"].As<List<Dictionary<string, object>>>()
                    .Select(leg => new RouteLeg(
                        leg["distance"] is null ? null : Convert.ToDouble(leg["distance"]),
                        leg["unit"].As<string>())).ToList()));
            return routes.FirstOrDefault();
        });
    }

    public async Task CreateCityExampleAsync()
    {
        await using var session = driver.AsyncSession(options =>
            options.WithDatabase(configuration["Neo4j:Database"]!));
        await session.ExecuteWriteAsync(async transaction =>
        {
            var normalize = await transaction.RunAsync("""
                MATCH (city:City)
                WHERE city.name = 'Vienna' OR city.name IS NULL
                SET city.name = CASE coalesce(city.name, city.description)
                    WHEN 'Vienna' THEN 'Wien'
                    ELSE coalesce(city.name, city.description) END
                """);
            await normalize.ConsumeAsync();
            var cursor = await transaction.RunAsync("""
                MERGE (graz:City {name: 'Graz'})
                MERGE (vienna:City {name: 'Wien'})
                MERGE (linz:City {name: 'Linz'})
                MERGE (graz)-[first:DISTANCE]->(vienna)
                ON CREATE SET first.value = 200, first.unit = 'km'
                MERGE (vienna)-[second:DISTANCE]->(linz)
                ON CREATE SET second.value = 180, second.unit = 'km'
                """);
            await cursor.ConsumeAsync();
        });
    }

    public async Task<List<Department>> GetDepartmentsAsync()
    {
        await driver.VerifyConnectivityAsync();
        await using var session = driver.AsyncSession(options =>
            options.WithDatabase(configuration["Neo4j:Database"]!));

        return await session.ExecuteReadAsync(async transaction =>
        {
            var cursor = await transaction.RunAsync("""
                MATCH (d:Department)
                RETURN d.deptno AS number, d.dname AS name, d.loc AS location
                ORDER BY d.deptno
                """);

            return await cursor.ToListAsync(record => new Department(
                record["number"].As<long>(),
                record["name"].As<string>(),
                record["location"].As<string>()));
        });
    }
}
