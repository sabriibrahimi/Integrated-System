using System.ComponentModel.DataAnnotations;

namespace HotelApplication.Web.Request;

public record RoomRequest
(
    [Required] int Capacity,
    [Required] int RoomNumber,
    [Required] string Status,
    [Required] double PricePerNight,
    [Required] Guid HotelId
    );