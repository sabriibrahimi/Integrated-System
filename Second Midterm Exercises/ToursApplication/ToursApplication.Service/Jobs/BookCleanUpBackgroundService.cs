using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ToursApplication.Service.Interface;

namespace ToursApplication.Service.Jobs;


public class BookCleanUpBackgroundService  : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<BookCleanUpBackgroundService> _logger;

    public BookCleanUpBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<BookCleanUpBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();
            
            _logger.LogInformation("Booking cleanup job started...");
            
            var bookings = await bookingService.GetOldCancelledBookingsAsync(DateTime.UtcNow.AddMinutes(-7));

            _logger.LogInformation(
                "Fetched {BookingCount} old cancelled bookings", bookings.Count);

            foreach (var booking in bookings)
            {
                try
                {
                    _logger.LogInformation(
                        "Deleting booking with ID: {BookingId}",
                        booking.Id);

                    await bookingService.DeleteByIdAsync(booking.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error while deleting booking with ID: {BookingId}",
                        booking.Id);
                }
            }

            _logger.LogInformation("Booking cleanup job finished successfully.");

            await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
        }
    }
}