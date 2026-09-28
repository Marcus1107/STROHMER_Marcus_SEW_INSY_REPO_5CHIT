using Microsoft.EntityFrameworkCore;
using Models;
using MySql.Data.MySqlClient;
using System.Data;
using WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: false);
    builder.Configuration.AddEnvironmentVariables();
    builder.Configuration.AddCommandLine(args);
}

builder.Services.AddDbContext<TicketDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("TicketDatabase")
        ?? throw new InvalidOperationException("Connection string 'TicketDatabase' is missing.")));

var app = builder.Build();

app.UseHttpsRedirection();


app.MapGet("/api/tickets", async (TicketDbContext db, CancellationToken cancellationToken) =>
{
    var tickets = await db.Tickets
        .AsNoTracking()
        .OrderBy(ticket => ticket.Id)
        .ToArrayAsync(cancellationToken);
    return Results.Ok(tickets);
});

app.MapPost("/api/tickets", (int count, int expectedCount, TicketDbContext db, CancellationToken ct) =>
    ChangeTicketsAsync(count, expectedCount, true, db, ct));

app.MapDelete("/api/tickets", (int count, int expectedCount, TicketDbContext db, CancellationToken ct) =>
    ChangeTicketsAsync(count, expectedCount, false, db, ct));

app.Run();

static async Task<IResult> ChangeTicketsAsync(
    int count, int expectedCount, bool adding, TicketDbContext db, CancellationToken ct)
{
    if (count <= 0 || expectedCount < 0)
        return Results.BadRequest("Ungültige Ticketanzahl.");

    const string conflict = "Der Ticketbestand wurde inzwischen geändert. Deine Änderung wurde nicht gespeichert. Bitte den Bestand aktualisieren.";
    try
    {
        // Prüfung und Änderung bilden eine Einheit, auch bei gleichzeitigen Anfragen.
        await using var transaction = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, ct);
        var currentCount = await db.Tickets.CountAsync(ct);
        if (currentCount != expectedCount)
            return Results.Conflict(conflict);

        if (adding)
        {
            for (var i = 0; i < count; i++)
                db.Tickets.Add(new Ticket { Name = "Neues Ticket" });
        }
        else
        {
            if (count > currentCount)
                return Results.BadRequest("Es sind nicht genügend Tickets vorhanden.");

            var tickets = await db.Tickets.OrderBy(t => t.Id).Take(count).ToArrayAsync(ct);
            db.Tickets.RemoveRange(tickets);
        }

        await db.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return Results.NoContent();
    }
    catch (DbUpdateConcurrencyException)
    {
        return Results.Conflict(conflict);
    }
    catch (Exception ex) when (ex is MySqlException { Number: 1213 or 1205 }
        || ex.InnerException is MySqlException { Number: 1213 or 1205 })
    {
        // Auch ein gleichzeitiger Schreibkonflikt wird als Konflikt zurückgegeben.
        return Results.Conflict(conflict);
    }
}
