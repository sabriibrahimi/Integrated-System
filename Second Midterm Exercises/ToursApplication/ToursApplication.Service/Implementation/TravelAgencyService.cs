using ExamsApplication.Domain.Dto;
using ExamsApplication.Repository.Interfaces;
using ToursApplication.Domain.Models;
using ToursApplication.Service.Interface;

namespace ToursApplication.Service;

public class TravelAgencyService : ITravelAgencyService
{
    private readonly IRepository<TravelAgency> _repository;

    public TravelAgencyService(IRepository<TravelAgency> repository)
    {
        _repository = repository;
    }

    public async Task<TravelAgency> GetByIdNotNullAsync(Guid id)
    {
        var entity = await _repository.GetAsync(selector: t => t, predicate: t => t.Id == id);
        if (entity == null)
            throw new KeyNotFoundException($"TravelAgency with id {id} not found");
        return entity;
    }

    public async Task<List<TravelAgency>> GetAllAsync()
    {
        return await _repository.GetAllAsync(selector: t => t);
    }

    public async Task<TravelAgency> CreateAsync(TravelAgencyDto dto)
    {
        var entity = new TravelAgency { Name = dto.Name };
        return await _repository.InsertAsync(entity);
    }

    public async Task<TravelAgency> UpdateAsync(Guid id, TravelAgencyDto dto)
    {
        var entity = await GetByIdNotNullAsync(id);
        entity.Name = dto.Name;
        return await _repository.UpdateAsync(entity);
    }

    public async Task<TravelAgency> DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdNotNullAsync(id);
        return await _repository.DeleteAsync(entity);
    }

    public async Task<PaginatedResult<TravelAgency>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync(selector: t => t, pageNumber: pageNumber, pageSize: pageSize);
    }
}
