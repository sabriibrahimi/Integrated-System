using ExamsApplication.Repository;
using Microsoft.EntityFrameworkCore;
using ToursApplication.Domain.ExternalModels;
using ToursApplication.Domain.Models;
using ToursApplication.Repository.Interface;

namespace ToursApplication.Repository.Implementation;

public class LegacyTourRepository : ILegacyTourRepository
{
    private readonly LegacyTourDbContext _dbContext;
    private readonly ApplicationDbContext _applicationDbContext;

    public LegacyTourRepository(LegacyTourDbContext dbContext, ApplicationDbContext applicationDbContext)
    {
        _dbContext = dbContext;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<Tour>> GetTourAsync()
    {
        var legacy = await _dbContext.ToursDirectory.AsNoTracking().ToListAsync();

        return legacy.Select(x => new Tour
        {
            Id = GuidHelper.FromLegacyId("Tour", x.Name),
            Name = x.Name,
            Capacity = x.Capacity
        }).ToList();
    }

    public async Task<List<Offers>> GetOffersAsync()
    {
        var legacy = await _dbContext.TourOfferings.AsNoTracking().ToListAsync();

        var agencies = await _applicationDbContext.TravelAgencies.AsNoTracking() .ToDictionaryAsync(x => x.Name, x => x.Id);
        
        return legacy
            .Where(x => agencies.ContainsKey(x.AgencyName))
            .Select(x => new Offers
            {
                Id = GuidHelper.FromLegacyId(
                    "Offer",
                    $"{x.AgencyName}:{x.TourName}"),

                TourId = GuidHelper.FromLegacyId("Tour", x.TourName),
                TravelAgencyId = agencies[x.AgencyName]
            }).ToList();

    }
}