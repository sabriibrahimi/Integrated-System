using HotelApplication.Domain.Dto;
using HotelApplication.Domain.Models;
using HotelApplication.Repository.Interface;
using HotelApplication.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace HotelApplication.Service.Implementation;

public class RoomService : IRoomService
{
    private readonly IRepository<Room> _repository;

    public RoomService(IRepository<Room> repository)
    {
        _repository = repository;
    }

    public async Task<List<Room>> GetAllAsync()
    {
        var result= await _repository.GetAllAsync(x=>x, include: x=>x.Include(y=>y.Reservations));
        return result.ToList();
    }

    public async Task<Room?> GetByIdNotNullAsync(Guid id)
    {
        var result = await _repository.Get(selector: x=>x, predicate: x=> x.Id == id, include: x=>x.Include(y=>y.Reservations));
        if (result == null)
        {
            throw new InvalidOperationException($"Room with id {id}");
        }

        
        return result;
    }

    public Task<Room> InsertAsync(RoomDto dto)
    {
        Room room = new Room
        {
            Id = Guid.NewGuid(),
            Status = dto.Status,
            RoomNumber = dto.RoomNumber,
            Capacity = dto.Capacity,
            PricePerNight = dto.PricePerNight,
            HotelId = dto.HotelId
        };
        
        return _repository.InsertAsync(room);
    }

    public async Task<Room> UpdateAsync(Guid id, RoomDto dto)
    {
        var room= await GetByIdNotNullAsync(id);
        room.RoomNumber = dto.RoomNumber;
        room.Capacity = dto.Capacity;
        room.PricePerNight = dto.PricePerNight;
        room.HotelId = dto.HotelId;
        room.Status = dto.Status;
        return await _repository.UpdateAsync(room);
    }

    public async Task<Room> DeleteAsync(Guid id)
    {
        var room= await GetByIdNotNullAsync(id);
        return await _repository.DeleteAsync(room);
    }

    public Task<PaginatedResult<Room>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var result = _repository.GetAllPagedAsync(
            selector:x=>x,
            pageNumber:pageNumber,
            pageSize:pageSize,
            include: x=> x.Include(y=>y.Reservations),
            orderBy: x=> x.OrderBy(e=>e.RoomNumber),
            asNoTracking: true);
        return result;
    }
}