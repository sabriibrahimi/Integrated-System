using Domain.Models;
using Microsoft.Extensions.Logging;
using Repository.Interface;

namespace Service.Implementation;

public class RoomEtlService
{
    private readonly ILegacyRoomRepository _legacyRoomRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IRepository<EtlSyncLog> _etlSyncLogRepository;
    private readonly ILogger<RoomEtlService> _logger;

    public RoomEtlService(ILegacyRoomRepository legacyRoomRepository, IRoomRepository roomRepository, IRepository<EtlSyncLog> etlSyncLogRepository, ILogger<RoomEtlService> logger)
    {
        _legacyRoomRepository = legacyRoomRepository;
        _roomRepository = roomRepository;
        _etlSyncLogRepository = etlSyncLogRepository;
        _logger = logger;
    }
    
     public async Task SyncAllAsync()
    {
        var syncLog = new EtlSyncLog
        {
            JobName = "RoomSync",
            StartedAt = DateTime.UtcNow
        };

        try
        {
            var lastRun = await _etlSyncLogRepository.GetAllAsync(
                selector: x => x,
                predicate: x => x.JobName == "RoomSync" && x.Success == true,
                orderBy: x => x.OrderByDescending(v => v.StartedAt));

            var date = lastRun.FirstOrDefault()?.StartedAt ?? DateTime.MinValue;

            _logger.LogInformation("Starting Legacy DB ETL with date last modified {date}", date);

            var rooms = await _legacyRoomRepository.GetRoomsModifiedSinceAsync(date);
            var consultations = await _legacyRoomRepository.GetConsultationModifiedSinceAsync(date);
            

            _logger.LogInformation(
                "Extracted and transformed total {rooms} Rooms, {consultations} Consultations", rooms.Count,
                consultations.Count);

            await _roomRepository.BulkInsertOrUpdateRoomAsync(rooms);
            await _roomRepository.BulkInsertOrUpdateConsultationAsync(consultations);
            
            
            _logger.LogInformation("Successfully loaded the data");

            syncLog.Success = true;
            syncLog.CompletedAt = DateTime.UtcNow;
            
            
            _logger.LogInformation("Legacy DB ETL finished successfully at {date}", syncLog.CompletedAt);

        }
        catch (Exception ex)
        {
            syncLog.Success = false;
            syncLog.ErrorMessage = ex.Message;
            syncLog.CompletedAt = DateTime.UtcNow;
            _logger.LogError(ex, "An error occured during the ETL process...");
        }
        finally
        {
            await _etlSyncLogRepository.InsertAsync(syncLog);
        }
    }

}