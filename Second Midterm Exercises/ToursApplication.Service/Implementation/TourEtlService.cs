using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using ToursApplication.Repository.Interface;

namespace ToursApplication.Service.Implementation;

public class TourEtlService
{
    private readonly ILegacyTourRepository _legacyTourRepository;
    private readonly ITourRepository _tourRepository;
    private readonly ILogger<TourEtlService> _logger;

    public TourEtlService(ILegacyTourRepository legacyTourRepository, ITourRepository tourRepository, ILogger<TourEtlService> logger)
    {
        _legacyTourRepository = legacyTourRepository;
        _tourRepository = tourRepository;
        _logger = logger;
    }

    public async Task SyncAllAsync()
    {
        
        _logger.LogInformation("Tour ETL synchronization started.");

        var tours = await _legacyTourRepository.GetTourAsync();
        await _tourRepository.BulkInsertOrUpdateToursAsync(tours);

        // Tours must be loaded first because offers reference them.
        var offers = await _legacyTourRepository.GetOffersAsync();
        await _tourRepository.BulkInsertOrUpdateOffersAsync(offers);

        _logger.LogInformation(
            "Tour ETL synchronization completed. Tours: {TourCount}, Offers: {OfferCount}",
            tours.Count,
            offers.Count);
    }
}