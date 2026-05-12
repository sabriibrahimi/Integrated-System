using System.Net.Quic;
using HotelApplication.Domain.Dto;
using HotelApplication.Domain.Enums;
using HotelApplication.Domain.Models;
using HotelApplication.Web.Request;
using HotelApplication.Web.Response;

namespace HotelApplication.Web.Extensions;

public static class RoomMappingExtensions
{
    public static RoomResponse? ToResponse(this Room r, List<RoomReviewDto?> roomDto = null)
    {
        return new RoomResponse(
            r.Id,
            r.Capacity,
            r.RoomNumber,
            r.Status.ToString(),
            r.PricePerNight,
            r.HotelId.Value,
            r.Hotel?.Name, 
            roomDto
        );
    }
    
    public static RoomWithReservationsResponse? ToResponseWithReservations(this Room r)
    {
        return new RoomWithReservationsResponse(
            r.Id,
            r.Capacity,
            r.RoomNumber,
            r.Status.ToString(),
            r.PricePerNight,
            r.HotelId.Value,
            r.Hotel?.Name,
            r.Reservations.ToResponse()
        );
    }
    
    
    public static List<RoomResponse> ToResponse(this List<Room> rooms)
    {
        return rooms.Select(r=> r.ToResponse()).ToList();
    }
    
    public static List<RoomWithReservationsResponse> ToResponsesWithReservations(this List<Room> rooms)
    {
        return rooms.Select(r=> r.ToResponseWithReservations()).ToList();
    }

    public static PaginatedResponse<RoomWithReservationsResponse> ToPaginatedRoomWithResResponse(
        this PaginatedResult<Room> rooms)
    {
        return rooms.ToPaginatedResponse(x => x.ToResponseWithReservations());
    }

    public static RoomDto ToDto(this RoomRequest request)
    {
        return new RoomDto{
           Capacity = request.Capacity,
            RoomNumber = request.RoomNumber,
            Status = Enum.Parse<Status>(request.Status, true),
            PricePerNight = request.PricePerNight,
            HotelId = request.HotelId
        };
    }

}