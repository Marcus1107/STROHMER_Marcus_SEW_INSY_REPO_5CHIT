using Microsoft.EntityFrameworkCore;
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

app.Run();
