using ExamsApplication.Domain.Dto;
using ExamsApplication.Repository.Interfaces;
using ToursApplication.Domain.Enums;
using ToursApplication.Domain.Models;
using ToursApplication.Service.Interface;

namespace ToursApplication.Service;

public class BookingService : IBookingService
{
    private readonly IRepository<Booking> _repository;

    public BookingService(IRepository<Booking> repository)
    {
        _repository = repository;
    }

    public async Task<Booking> GetByIdNotNullAsync(Guid id)
    {
        var entity = await _repository.GetAsync(selector: b => b, predicate: b => b.Id == id);
        if (entity == null)
            throw new KeyNotFoundException($"Booking with id {id} not found");
        return entity;
    }

    public async Task<List<Booking>> GetAllAsync()
    {
        return await _repository.GetAllAsync(selector: b => b);
    }

    public async Task<Booking> CreateAsync(BookingDto dto)
    {
        var entity = new Booking
        {
            UserId = dto.UserId,
            TourId = dto.TourId,
            TravelAgencyId = dto.TravelAgencyId,
            Status = dto.Status
        };
        return await _repository.InsertAsync(entity);
    }

    public async Task<Booking> UpdateAsync(Guid id, BookingDto dto)
    {
        var entity = await GetByIdNotNullAsync(id);
        entity.UserId = dto.UserId;
        entity.TourId = dto.TourId;
        entity.TravelAgencyId = dto.TravelAgencyId;
        entity.Status = dto.Status;
        return await _repository.UpdateAsync(entity);
    }

    public async Task<Booking> DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdNotNullAsync(id);
        return await _repository.DeleteAsync(entity);
    }

    public async Task<List<Booking>> GetOldCancelledBookingsAsync(DateTime cutoffDate)
    {
        return await _repository.GetAllAsync(
            selector: booking => booking,
            predicate: booking => booking.Status == BookingStatus.Cancelled && booking.DateCreated < cutoffDate
        );
    }

    public async Task<PaginatedResult<Booking>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync(selector: b => b, pageNumber: pageNumber, pageSize: pageSize);
    }
}
