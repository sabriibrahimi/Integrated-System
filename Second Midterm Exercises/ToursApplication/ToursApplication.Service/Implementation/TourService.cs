using ExamsApplication.Domain.Dto;
using ExamsApplication.Repository.Interfaces;
using ToursApplication.Domain.Models;
using ToursApplication.Service.Interface;

namespace ToursApplication.Service;

public class TourService : ITourService
{
    private readonly IRepository<Tour> _repository;

    public TourService(IRepository<Tour> repository)
    {
        _repository = repository;
    }

    public async Task<Tour> GetByIdNotNullAsync(Guid id)
    {
        var tour = await _repository.GetAsync(selector: t => t,
            predicate: t => t.Id == id);

        if (tour == null)
        {
            throw new KeyNotFoundException($"Tour with id {id} not found");
        }
        
        return tour;
    }

    public async Task<List<Tour>> GetAllAsync()
    {
        return await _repository.GetAllAsync(selector: t => t);
    }

    public async Task<Tour> CreateAsync(TourDto dto)
    {
        var tour = new Tour()
        {
            Name = dto.Name,
            Capacity = dto.Capacity
        };

        return await _repository.InsertAsync(tour);
    }

    public async Task<Tour> UpdateAsync(Guid id, TourDto dto)
    {
        var tour = await GetByIdNotNullAsync(id);
        tour.Name = dto.Name;
        tour.Capacity = dto.Capacity;
        return await _repository.UpdateAsync(tour);
    }

    public async Task<PaginatedResult<Tour>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _repository.GetAllPagedAsync(selector: t => t,
            pageNumber: pageNumber,
            pageSize: pageSize);
    }
}