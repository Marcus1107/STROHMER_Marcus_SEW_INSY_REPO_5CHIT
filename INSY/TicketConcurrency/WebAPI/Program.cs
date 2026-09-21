using Models;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

// Beispieldaten im Arbeitsspeicher; noch keine Datenbank.
var tickets = Enumerable.Range(1, 100)
    .Select(id => new Ticket { Id = id, Name = $"Ticket {id}" })
    .ToArray();

app.MapGet("/api/tickets", () => tickets);

app.Run();