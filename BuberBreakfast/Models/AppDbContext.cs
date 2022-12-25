using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Models;

public class AppDbContext : DbContext
{
    public DbSet<Breakfast> Breakfasts { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public AppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseMySQL("server=localhost;database=buber_breakfast;uid=root;pwd=Mohamad1375");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasOne(p => p.User).WithMany(u => u.Posts);
        modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany(u => u.Comments);
        modelBuilder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments);
    }
}