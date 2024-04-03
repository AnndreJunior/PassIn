using Microsoft.EntityFrameworkCore;
using PassIn.Infra.Entities;

namespace PassIn.Infra;

public class PassInDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=../PassInDb.db");
    }
}
