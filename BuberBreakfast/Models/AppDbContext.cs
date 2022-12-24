using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Models;

public class AppDbContext : DbContext
{
    public DbSet<Breakfast> Breakfasts { get; set; }

    public AppDbContext (DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
}