using HotelApplication.Domain.Dto;
using HotelApplication.Domain.Models;
using HotelApplication.Service.Interface;

namespace HotelApplication.Service.Implementation;

public class RoomService : IRoomService
{
    public Task<List<Room>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Room> GetByIdNotNullAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Room> InsertAsync(RoomDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Room> UpdateAsync(Guid id, RoomDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Room> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedResult<Room>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }
}