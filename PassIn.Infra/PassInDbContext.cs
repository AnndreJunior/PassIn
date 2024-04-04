using Microsoft.EntityFrameworkCore;
using PassIn.Infra.Entities;

namespace PassIn.Infra;

public class PassInDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../PassInDb.db");
    }
}
