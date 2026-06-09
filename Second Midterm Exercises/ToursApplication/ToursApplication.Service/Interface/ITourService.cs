using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;

namespace ToursApplication.Service.Interface;

public interface ITourService
{
    Task<Tour> GetByIdNotNullAsync(Guid id);
    Task<List<Tour>> GetAllAsync();
    Task<Tour> CreateAsync(TourDto dto);
    Task<Tour> UpdateAsync(Guid id, TourDto dto);
    Task<PaginatedResult<Tour>> GetAllPagedAsync(int pageNumber, int pageSize);
}