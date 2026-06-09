using ExamsApplication.Domain.Common;

namespace ToursApplication.Domain.Models;

public class Offers : BaseEntity
{
    public Guid TourId { get; set; }
    public virtual Tour Tour { get; set; }
    
    public Guid TravelAgencyId { get; set; }
    public virtual TravelAgency TravelAgency { get; set; }
}