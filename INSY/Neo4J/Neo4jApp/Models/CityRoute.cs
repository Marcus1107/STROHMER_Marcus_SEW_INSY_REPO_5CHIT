namespace Neo4jApp.Models;

public record CityRoute(List<string> Cities, List<RouteLeg> Legs);
public record RouteLeg(double? Distance, string Unit);
