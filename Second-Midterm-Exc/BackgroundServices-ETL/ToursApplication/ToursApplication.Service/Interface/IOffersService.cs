using ExamsApplication.Domain.Dto;
using ToursApplication.Domain.Models;

namespace ToursApplication.Service.Interface;

public interface IOffersService
{
    Task<Offers> GetByIdNotNullAsync(Guid id);
    Task<List<Offers>> GetAllAsync();
    Task<Offers> CreateAsync(OffersDto dto);
    Task<Offers> UpdateAsync(Guid id, OffersDto dto);
    Task<Offers> DeleteByIdAsync(Guid id);
    Task<PaginatedResult<Offers>> GetAllPagedAsync(int pageNumber, int pageSize);
}
