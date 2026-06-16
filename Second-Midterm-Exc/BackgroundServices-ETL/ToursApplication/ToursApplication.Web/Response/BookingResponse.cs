using ToursApplication.Domain.Enums;

namespace ToursApplication.Web.Response;

public record BookingResponse(Guid Id, string UserId, Guid TourId, Guid TravelAgencyId, BookingStatus Status);
