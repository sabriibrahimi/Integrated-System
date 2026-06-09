using ExamsApplication.Domain.Common;
using ToursApplication.Domain.Enums;

namespace ToursApplication.Domain.Models;

public class Booking : BaseAuditableEntity<string>
{
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public Guid TravelAgencyId { get; set; }
    public BookingStatus Status { get; set; }
    public virtual Tour Tour { get; set; }
    public virtual TravelAgency TravelAgency { get; set; }
    public virtual ToursApplicationUser User { get; set; }
}