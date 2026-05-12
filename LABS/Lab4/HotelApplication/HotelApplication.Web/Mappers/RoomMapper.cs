using HotelApplication.Domain.Dto;
using HotelApplication.Domain.Models;
using HotelApplication.Service.Interface;
using HotelApplication.Web.Extensions;
using HotelApplication.Web.Request;
using HotelApplication.Web.Response;

namespace HotelApplication.Web.Mappers;

public class RoomMapper
{
    private readonly IRoomService _roomService;
    private readonly IRoomReviewService _reviewService;
    
    public RoomMapper(IRoomService roomService, IRoomReviewService reviewService)
    {
        _roomService = roomService;
        _reviewService = reviewService;
    }
    
    public async Task<List<RoomWithReservationsResponse>> GetAllAsync()
    {
        var result= await _roomService.GetAllAsync();
        return result.ToResponsesWithReservations();
    }

    public async Task<RoomResponse?> GetByIdNotNullAsync(Guid id)
    {
        var result = await _roomService.GetByIdNotNullAsync(id);
        var reviews = await _reviewService.GetReviewRoomDataByIdAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"Room with id {id}");
        }

        
        return result.ToResponse(reviews);
    }

    public async Task<RoomResponse> InsertAsync(RoomRequest request)
    {
        var dto = request.ToDto();
        var result = await _roomService.InsertAsync(dto);
        return result.ToResponse();
    }

    public async Task<RoomResponse> UpdateAsync(Guid id, RoomRequest request)
    {
        var dto = request.ToDto();
        var result=  await _roomService.UpdateAsync(id, dto);
        return result.ToResponse();
    }

    public async Task<RoomResponse> DeleteAsync(Guid id)
    {
       var result = await _roomService.DeleteAsync(id);
       return result?.ToResponse();
    }

    public async Task<PaginatedResponse<RoomResponse>> GetAllPagedAsync(PaginatedRequest request)
    {
        var result = await  _roomService.GetAllPagedAsync(request.PageNumber, request.PageSize);
        return result.ToPaginatedResponse(room => room.ToResponse());
    }
}