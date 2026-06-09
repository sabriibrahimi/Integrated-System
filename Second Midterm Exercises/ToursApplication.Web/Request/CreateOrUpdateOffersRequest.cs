namespace ToursApplication.Web.Request;

public record CreateOrUpdateOffersRequest(Guid TourId, Guid TravelAgencyId);
