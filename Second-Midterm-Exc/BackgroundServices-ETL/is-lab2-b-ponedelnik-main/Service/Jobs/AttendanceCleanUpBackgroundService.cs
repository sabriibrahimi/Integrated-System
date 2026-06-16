using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Interface;

namespace Service.Jobs;

public class AttendanceCleanUpBackgroundService  : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<AttendanceCleanUpBackgroundService> _logger;

    public AttendanceCleanUpBackgroundService(IServiceScopeFactory serviceScopeFactory, ILogger<AttendanceCleanUpBackgroundService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var attendanceService = scope.ServiceProvider.GetRequiredService<IAttendanceService>();
            
            _logger.LogInformation("Attendance cleanup job started...");
            
            var attendances = await attendanceService.GetStudentAttendanceByAsync(DateTime.UtcNow.AddDays(-7));

            _logger.LogInformation("Fetched {AttendanceCount} attendance records", attendances.Count);

            foreach (var attendance in attendances)
            {
                try
                {
                    await attendanceService.DeleteByIdAsync(attendance.Id);

                    _logger.LogInformation("Deleted attendance with ID: {AttendanceId}", attendance.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex, "Error deleting attendance with ID: {AttendanceId}", attendance.Id);
                }
            }

            await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
        }
    }
}