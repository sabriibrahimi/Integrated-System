using Microsoft.Extensions.Logging;
using Quartz;
using Service.Implementation;

namespace Service.Jobs;

public class QuartzRoomEtlJob : IJob
{
    private readonly RoomEtlService _roomEtlService;
    private readonly ILogger<QuartzRoomEtlJob> _logger;

    public QuartzRoomEtlJob(RoomEtlService roomEtlService, ILogger<QuartzRoomEtlJob> logger)
    {
        _roomEtlService = roomEtlService;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Room ETL job started...");

        await _roomEtlService.SyncAllAsync();

        _logger.LogInformation("Room ETL job finished...");

    }
}