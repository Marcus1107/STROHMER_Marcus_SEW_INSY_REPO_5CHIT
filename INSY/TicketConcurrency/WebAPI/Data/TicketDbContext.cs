using Microsoft.EntityFrameworkCore;
using Models;

namespace WebAPI.Data;

public class TicketDbContext : DbContext
{
    public TicketDbContext(DbContextOptions<TicketDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<ChangelogEntry> ChangelogEntries => Set<ChangelogEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Diese Tabelle wird über die SQL-Skripte in DataGrip verwaltet.
        modelBuilder.Entity<ChangelogEntry>()
            .ToTable("changelog", table => table.ExcludeFromMigrations());
    }
}
