using HotelApplication.Domain.Dto;

namespace HotelApplication.Service.Interface;

public interface IRoomReviewService
{
    Task<List<RoomReviewDto>> GetReviewRoomDataByIdAsync(Guid eventId);
}