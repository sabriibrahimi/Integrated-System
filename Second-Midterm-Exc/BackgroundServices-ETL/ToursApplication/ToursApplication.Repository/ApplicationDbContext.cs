using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToursApplication.Domain.Models;

namespace ExamsApplication.Repository;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ToursApplicationUser>(options)
{
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Guides> Guides { get; set; }
    public DbSet<Offers> Offers { get; set; }
    public DbSet<TravelAgency> TravelAgencies { get; set; }
    public DbSet<EtlSyncLog> EtlSyncLogs { get; set; }

}