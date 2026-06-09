using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;

namespace ToursApplication.Service.Interface;

public interface IGuidesService
{
    Task<Guides> GetByIdNotNullAsync(Guid id);
    Task<List<Guides>> GetAllAsync();
    Task<Guides> CreateAsync(GuidesDto dto);
    Task<Guides> UpdateAsync(Guid id, GuidesDto dto);
    Task<Guides> DeleteByIdAsync(Guid id);
    Task<PaginatedResult<Guides>> GetAllPagedAsync(int pageNumber, int pageSize);
}
