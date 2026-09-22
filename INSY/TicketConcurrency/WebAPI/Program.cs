using Microsoft.EntityFrameworkCore;
using Models;
using WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TicketDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("TicketDatabase")
        ?? throw new InvalidOperationException("Connection string 'TicketDatabase' is missing.")));

var app = builder.Build();

app.UseHttpsRedirection();


var tickets = Enumerable.Range(1, 100)
    .Select(id => new Ticket { Id = id, Name = $"Ticket {id}" })
    .ToArray();

app.MapGet("/api/tickets", () => tickets);

app.Run();
