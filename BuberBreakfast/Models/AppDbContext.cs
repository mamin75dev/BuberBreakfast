using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Models;

public class AppDbContext : DbContext
{
    public DbSet<Breakfast> Breakfasts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=localhost;database=buber_breakfast;uid=root;pwd=Mohamad1375");
    }
}