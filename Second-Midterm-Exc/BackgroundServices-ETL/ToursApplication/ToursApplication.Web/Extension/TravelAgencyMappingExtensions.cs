using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Extension;

public static class TravelAgencyMappingExtensions
{
    public static TravelAgencyResponse ToResponse(this TravelAgency t)
    {
        return new TravelAgencyResponse(t.Id, t.Name);
    }

    public static TravelAgencyDto ToDto(this CreateOrUpdateTravelAgencyRequest request)
    {
        return new TravelAgencyDto { Name = request.Name };
    }

    public static List<TravelAgencyResponse> ToResponse(this List<TravelAgency> list)
    {
        return list.Select(ToResponse).ToList();
    }
}
