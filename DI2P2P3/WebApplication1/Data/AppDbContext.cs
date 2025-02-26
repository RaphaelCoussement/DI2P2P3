using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public DbSet<Application> Applications { get; set; }
    public DbSet<Password> Passwords { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Password>()
            .HasOne(p => p.Application)
            .WithMany()
            .HasForeignKey(p => p.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Application>().HasData(
            new Application { Id = 1, Name = "Facebook", Type = ApplicationType.GrandPublic },
            new Application { Id = 2, Name = "EntrepriseX", Type = ApplicationType.Professionnelle }
        );
    }
}
