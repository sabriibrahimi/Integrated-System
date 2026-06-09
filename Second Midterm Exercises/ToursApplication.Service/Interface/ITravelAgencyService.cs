using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;

namespace ToursApplication.Service.Interface;

public interface ITravelAgencyService
{
    Task<TravelAgency> GetByIdNotNullAsync(Guid id);
    Task<List<TravelAgency>> GetAllAsync();
    Task<TravelAgency> CreateAsync(TravelAgencyDto dto);
    Task<TravelAgency> UpdateAsync(Guid id, TravelAgencyDto dto);
    Task<TravelAgency> DeleteByIdAsync(Guid id);
    Task<PaginatedResult<TravelAgency>> GetAllPagedAsync(int pageNumber, int pageSize);
}
