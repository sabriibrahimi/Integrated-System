using HotelApplication.Domain.Dto;
using HotelApplication.Domain.Enums;

namespace HotelApplication.Web.Response;

public record RoomResponse(
    Guid Id,
    int Capacity,
    int RoomNumber,
    string Status,
    double PricePerNight,
    Guid HotelId,
    string HotelName,
    List<RoomReviewDto?> roomDto
    );