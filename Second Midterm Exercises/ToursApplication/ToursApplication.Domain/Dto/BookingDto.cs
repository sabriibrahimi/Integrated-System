using ToursApplication.Domain.Enums;

namespace ExamsApplication.Domain.Dto;

public class BookingDto
{
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public Guid TravelAgencyId { get; set; }
    public BookingStatus Status { get; set; }
}
