using ExamsApplication.Domain.Common;

namespace ToursApplication.Domain.Models;

public class TravelAgency : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Offers> Offers { get; set; }
    public virtual ICollection<Booking> Booking { get; set; }
}