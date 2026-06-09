using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Extension;

public static class TourMappingExtensions
{
    public static TourResponse ToResponse(this Tour tour)
    {
        return new TourResponse(tour.Name, tour.Capacity);
    }

    public static TourDto ToDto(this CreateOrUpdateTourRequest request)
    {
        return new TourDto()
        {
            Name = request.Name,
            Capacity = request.Capacity
        };
    }

    public static List<TourResponse> ToResponse(this List<Tour> tours)
    {
        return tours.Select(ToResponse).ToList();
    }
    
}

