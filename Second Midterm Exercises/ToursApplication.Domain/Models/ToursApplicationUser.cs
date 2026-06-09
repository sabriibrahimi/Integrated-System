using Microsoft.AspNetCore.Identity;

namespace ToursApplication.Domain.Models;

public class ToursApplicationUser : IdentityUser
{
    public virtual ICollection<Booking> Bookings { get; set; }
    public virtual ICollection<Guides> Guides { get; set; }
}