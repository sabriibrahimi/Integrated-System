using Microsoft.Extensions.Logging;
using Quartz;
using ToursApplication.Repository.Interface;

namespace ToursApplication.Service.Jobs;

public class QuartzTourEtlJob : IJob
{
    private readonly ILegacyTourRepository _legacyRepository;
    private readonly ITourRepository _tourRepository;
    private readonly ILogger<QuartzTourEtlJob> _logger;

    public QuartzTourEtlJob(
        ILegacyTourRepository legacyRepository,
        ITourRepository tourRepository,
        ILogger<QuartzTourEtlJob> logger)
    {
        _legacyRepository = legacyRepository;
        _tourRepository = tourRepository;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Tour ETL job started...");

        var tours = await _legacyRepository.GetTourAsync();
        await _tourRepository.BulkInsertOrUpdateToursAsync(tours);

        var offers = await _legacyRepository.GetOffersAsync();
        await _tourRepository.BulkInsertOrUpdateOffersAsync(offers);

        _logger.LogInformation(
            "Tour ETL job finished. Tours: {TourCount}, Offers: {OfferCount}",
            tours.Count,
            offers.Count);
    }
}