using HotelApplication.Domain.Dto;

namespace HotelApplication.Service.Interface;

public interface IRoomReviewApiClient
{
     Task<ExternalPagedResponse<RoomReviewDto>> GetFirstFiveRoomReviewsByAsync(Guid roomId);
}