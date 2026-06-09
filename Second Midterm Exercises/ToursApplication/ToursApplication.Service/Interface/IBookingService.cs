using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;

namespace ToursApplication.Service.Interface;

public interface IBookingService
{
    Task<Booking> GetByIdNotNullAsync(Guid id);
    Task<List<Booking>> GetAllAsync();
    Task<Booking> CreateAsync(BookingDto dto);
    Task<Booking> UpdateAsync(Guid id, BookingDto dto);
    Task<Booking> DeleteByIdAsync(Guid id);
    Task<List<Booking>> GetOldCancelledBookingsAsync(DateTime cutoffDate);
    Task<PaginatedResult<Booking>> GetAllPagedAsync(int pageNumber, int pageSize);
}
