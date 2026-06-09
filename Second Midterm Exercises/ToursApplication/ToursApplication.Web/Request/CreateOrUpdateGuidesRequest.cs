namespace ToursApplication.Web.Request;

public record CreateOrUpdateGuidesRequest(string UserId, Guid TourId, DateTime StartDate, DateTime EndDate);
