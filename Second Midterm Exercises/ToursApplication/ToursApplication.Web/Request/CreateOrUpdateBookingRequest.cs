using ToursApplication.Domain.Enums;

namespace ToursApplication.Web.Request;

public record CreateOrUpdateBookingRequest(string UserId, Guid TourId, Guid TravelAgencyId, BookingStatus Status);
