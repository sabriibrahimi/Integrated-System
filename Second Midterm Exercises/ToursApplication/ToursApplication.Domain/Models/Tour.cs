using ExamsApplication.Domain.Common;

namespace ToursApplication.Domain.Models;

public class Tour : BaseEntity
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    
    public virtual ICollection<Offers> Offers { get; set; }
    public virtual ICollection<Guides> Guides { get; set; }
    public virtual ICollection<Booking> Booking { get; set; }
}