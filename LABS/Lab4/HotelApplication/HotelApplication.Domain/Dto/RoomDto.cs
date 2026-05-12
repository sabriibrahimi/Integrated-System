using HotelApplication.Domain.Enums;

namespace HotelApplication.Domain.Dto;

public class RoomDto
{
    //TODO
    public int RoomNumber { get; set; }
    public int Capacity { get; set; }
    public double PricePerNight { get; set; }
    public Guid HotelId { get; set; }
    public Status Status { get; set; }
    
}