using ExamsApplication.Domain.Dto;
using ExamsApplication.Repository.Interfaces;
using ToursApplication.Domain.Models;
using ToursApplication.Service.Interface;

namespace ToursApplication.Service;

public class OffersService : IOffersService
{
    private readonly IRepository<Offers> _repository;

    public OffersService(IRepository<Offers> repository)
    {
        _repository = repository;
    }

    public async Task<Offers> GetByIdNotNullAsync(Guid id)
    {
        var entity = await _repository.GetAsync(selector: o => o, predicate: o => o.Id == id);
        if (entity == null)
            throw new KeyNotFoundException($"Offer with id {id} not found");
        return entity;
    }

    public async Task<List<Offers>> GetAllAsync()
    {
        return await _repository.GetAllAsync(selector: o => o);
    }

    public async Task<Offers> CreateAsync(OffersDto dto)
    {
        var entity = new Offers
        {
            TourId = dto.TourId,
            TravelAgencyId = dto.TravelAgencyId
        };
        return await _repository.InsertAsync(entity);
    }

    public async Task<Offers> UpdateAsync(Guid id, OffersDto dto)
    {
        var entity = await GetByIdNotNullAsync(id);
        entity.TourId = dto.TourId;
        entity.TravelAgencyId = dto.TravelAgencyId;
        return await _repository.UpdateAsync(entity);
    }

    public async Task<Offers> DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdNotNullAsync(id);
        return await _repository.DeleteAsync(entity);
    }

    public async Task<PaginatedResult<Offers>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync(selector: o => o, pageNumber: pageNumber, pageSize: pageSize);
    }
}
