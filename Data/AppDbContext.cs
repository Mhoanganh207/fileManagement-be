using fileManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace fileManagement.Data;

public class AppDbContext : DbContext

{


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public  DbSet<AppFile> Files { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
        .HasMany<AppFile>(u => u.Files)
        .WithOne(f => f.User)
        .HasForeignKey(f => f.UserId)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
