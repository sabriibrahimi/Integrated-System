using ExamsApplication.Domain.Dto;
using ExamsApplication.Repository.Interfaces;
using ToursApplication.Domain.Models;
using ToursApplication.Service.Interface;

namespace ToursApplication.Service;

public class GuidesService : IGuidesService
{
    private readonly IRepository<Guides> _repository;

    public GuidesService(IRepository<Guides> repository)
    {
        _repository = repository;
    }

    public async Task<Guides> GetByIdNotNullAsync(Guid id)
    {
        var entity = await _repository.GetAsync(selector: g => g, predicate: g => g.Id == id);
        if (entity == null)
            throw new KeyNotFoundException($"Guide with id {id} not found");
        return entity;
    }

    public async Task<List<Guides>> GetAllAsync()
    {
        return await _repository.GetAllAsync(selector: g => g);
    }

    public async Task<Guides> CreateAsync(GuidesDto dto)
    {
        var entity = new Guides
        {
            UserId = dto.UserId,
            TourId = dto.TourId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate
        };
        return await _repository.InsertAsync(entity);
    }

    public async Task<Guides> UpdateAsync(Guid id, GuidesDto dto)
    {
        var entity = await GetByIdNotNullAsync(id);
        entity.UserId = dto.UserId;
        entity.TourId = dto.TourId;
        entity.StartDate = dto.StartDate;
        entity.EndDate = dto.EndDate;
        return await _repository.UpdateAsync(entity);
    }

    public async Task<Guides> DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdNotNullAsync(id);
        return await _repository.DeleteAsync(entity);
    }

    public async Task<PaginatedResult<Guides>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync(selector: g => g, pageNumber: pageNumber, pageSize: pageSize);
    }
}
