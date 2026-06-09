using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Extension;

public static class GuidesMappingExtensions
{
    public static GuidesResponse ToResponse(this Guides g)
    {
        return new GuidesResponse(g.Id, g.UserId, g.TourId, g.StartDate, g.EndDate);
    }

    public static GuidesDto ToDto(this CreateOrUpdateGuidesRequest request)
    {
        return new GuidesDto
        {
            UserId = request.UserId,
            TourId = request.TourId,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };
    }

    public static List<GuidesResponse> ToResponse(this List<Guides> list)
    {
        return list.Select(ToResponse).ToList();
    }
}
