using Domain.ExternalModels;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class LegacyApplicationDbContext(DbContextOptions<LegacyApplicationDbContext> options) : DbContext(options)
{
    public DbSet<LegacyRoomDirectory> RoomDirectories { get; set; }
    public DbSet<LegacyConsultationSlots> ConsultationSlots { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LegacyRoomDirectory>(e => e.HasKey(r => r.RoomCode));
        modelBuilder.Entity<LegacyConsultationSlots>(e => e.HasKey(c => c.SlotId));
    }
}
