using Microsoft.EntityFrameworkCore;
using ToursApplication.Domain.ExternalModels;

namespace ToursApplication.Repository;

public class LegacyTourDbContext(DbContextOptions<LegacyTourDbContext> options) : DbContext(options)
{
    public DbSet<LegacyToursDirectory> ToursDirectory { get; set; }
    public DbSet<LegacyTourOfferings> TourOfferings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LegacyToursDirectory>()
            .ToTable("ToursDirectory")
            .HasKey(x => x.Name);

        modelBuilder.Entity<LegacyTourOfferings>()
            .ToTable("TourOfferings")
            .HasKey(x => new { x.AgencyName, x.TourName });
    }
}