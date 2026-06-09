using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Extension;

public static class OffersMappingExtensions
{
    public static OffersResponse ToResponse(this Offers o)
    {
        return new OffersResponse(o.Id, o.TourId, o.TravelAgencyId);
    }

    public static OffersDto ToDto(this CreateOrUpdateOffersRequest request)
    {
        return new OffersDto
        {
            TourId = request.TourId,
            TravelAgencyId = request.TravelAgencyId
        };
    }

    public static List<OffersResponse> ToResponse(this List<Offers> list)
    {
        return list.Select(ToResponse).ToList();
    }
}
