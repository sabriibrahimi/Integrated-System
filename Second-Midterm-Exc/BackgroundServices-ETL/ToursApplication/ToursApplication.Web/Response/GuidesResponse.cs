namespace ToursApplication.Web.Response;

public record GuidesResponse(Guid Id, string UserId, Guid TourId, DateTime StartDate, DateTime EndDate);
