using LinqAssessment.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LinqAssessment.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<LinqAssessment.API.Models.Route> Routes { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LinqAssessment.API.Models.Route>()
            .HasMany(r => r.Flights)
            .WithOne(f => f.Route)
            .HasForeignKey(f => f.RouteId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
} 