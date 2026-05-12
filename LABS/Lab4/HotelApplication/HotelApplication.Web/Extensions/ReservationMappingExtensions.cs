using HotelApplication.Domain.Models;
using HotelApplication.Web.Response;

namespace HotelApplication.Web.Extensions;

public static class ReservationMappingExtensions
{
    public static ReservationResponse? ToResponse(this Reservation r)
    {
        return new ReservationResponse(
            r.Id,
            r.StartDate,
            r.EndDate,
            r.ServiceDateTime,
            r.RoomId,
            r.Room?.RoomNumber,
            r.UserId?.ToString(),
            r.User?.FirstName,
            r.User?.LastName,
            r.HotelServiceId,
            r.HotelService?.Price
        );
    }
    
    public static List<ReservationResponse> ToResponse(this ICollection<Reservation> reservations)
    {
        return reservations.Select(r=> r.ToResponse()).ToList();
    }
    
    
}