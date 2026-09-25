using Microsoft.EntityFrameworkCore;
using Models;
using WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TicketDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("TicketDatabase")
        ?? throw new InvalidOperationException("Connection string 'TicketDatabase' is missing.")));

var app = builder.Build();

app.UseHttpsRedirection();


app.MapGet("/api/tickets", async (TicketDbContext db, CancellationToken cancellationToken) =>
    await db.Tickets
        .AsNoTracking()
        .OrderBy(ticket => ticket.Id)
        .ToArrayAsync(cancellationToken));

app.MapPost("/api/tickets", async (int count, TicketDbContext db, CancellationToken cancellationToken) =>
{
    if (count <= 0)
    {
        return Results.BadRequest("Die Anzahl muss größer als 0 sein.");
    }

    for (var i = 0; i < count; i++)
    {
        db.Tickets.Add(new Ticket { Name = "Neues Ticket" });
    }

    await db.SaveChangesAsync(cancellationToken);
    return Results.NoContent();
});

app.MapDelete("/api/tickets", async (int count, TicketDbContext db, CancellationToken cancellationToken) =>
{
    if (count <= 0)
    {
        return Results.BadRequest("Die Anzahl muss größer als 0 sein.");
    }

    var tickets = await db.Tickets
        .OrderBy(ticket => ticket.Id)
        .Take(count)
        .ToArrayAsync(cancellationToken);

    if (tickets.Length < count)
    {
        return Results.Conflict("Es sind nicht genügend Tickets vorhanden.");
    }

    db.Tickets.RemoveRange(tickets);
    try
    {
        await db.SaveChangesAsync(cancellationToken);
    }
    catch (DbUpdateConcurrencyException)
    {
        return Results.Conflict("Der Bestand wurde gleichzeitig geändert. Bitte aktualisieren.");
    }

    return Results.NoContent();
});

app.Run();
