using ExamsApplication.Domain.Common;

namespace ToursApplication.Domain.Models;

public class Guides : BaseEntity
{
    public string UserId { get; set; }
    public virtual ToursApplicationUser User { get; set; }
    
    public Guid TourId { get; set; }
    public virtual Tour Tour { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}