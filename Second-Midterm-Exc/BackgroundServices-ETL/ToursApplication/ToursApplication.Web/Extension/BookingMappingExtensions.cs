using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;
using ToursApplication.Web.Request;
using ToursApplication.Web.Response;

namespace ToursApplication.Web.Extension;

public static class BookingMappingExtensions
{
    public static BookingResponse ToResponse(this Booking b)
    {
        return new BookingResponse(b.Id, b.UserId, b.TourId, b.TravelAgencyId, b.Status);
    }

    public static BookingDto ToDto(this CreateOrUpdateBookingRequest request)
    {
        return new BookingDto
        {
            UserId = request.UserId,
            TourId = request.TourId,
            TravelAgencyId = request.TravelAgencyId,
            Status = request.Status
        };
    }

    public static List<BookingResponse> ToResponse(this List<Booking> list)
    {
        return list.Select(ToResponse).ToList();
    }
}
