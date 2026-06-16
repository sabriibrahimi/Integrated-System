using System.Text.Json;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Domain.Requests;
using Microsoft.Extensions.Logging;
using Service.Interface;

namespace Service.Implementation;

public class InboundAttendanceProcessor
{
    private readonly IInboundAttendanceService _inboundAttendanceService;
    private readonly IAttendanceService _attendanceService;
    private readonly ILogger<InboundAttendanceProcessor> _logger;
    private readonly IConsultationService _consultationService;

    public InboundAttendanceProcessor(IInboundAttendanceService inboundAttendanceService, IAttendanceService attendanceService, ILogger<InboundAttendanceProcessor> logger, IConsultationService consultationService)
    {
        _inboundAttendanceService = inboundAttendanceService;
        _attendanceService = attendanceService;
        _logger = logger;
        _consultationService = consultationService;
    }

    public async Task ProcessPendingAttendanceAsync()
    {
        var pending = await _inboundAttendanceService.GetPendingBatchAsync(5);

        foreach (var entry in pending)
        {
            try
            {
                entry.Status = ProcessingStatus.Processing;
                await _inboundAttendanceService.UpdateAsync(entry);

                await ProcessAttendanceEntry(entry);

                entry.Status = ProcessingStatus.Completed;
                entry.ProcessedAt = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                entry.Status = ProcessingStatus.Failed;
                entry.ErrorMessage = ex.Message;
                entry.ProcessedAt = DateTime.UtcNow;

                _logger.LogError(ex, "Failed to process {Id}", entry.Id);
            }

            await _inboundAttendanceService.UpdateAsync(entry);
        }
    }

    private async Task ProcessAttendanceEntry(InboundAttendanceEntry entry)
    {
        var request = JsonSerializer.Deserialize<InboundAttendanceRequest>(entry.RawPayload);
        
        if (request==null)
        {
            throw new InvalidOperationException("Invalid payload");
        }

        var consultation = await _consultationService.GetByIdNotNullAsync(Guid.Parse(request.ConsultationId));
        
        if (consultation == null)
        {
            throw new InvalidOperationException(
                $"Consultation {request.ConsultationId} not found");
        }
        var attendanceDto = new AttendanceDto(
            request.Notes,
            Status.Present, 
            request.UserId,
            consultation.RoomId,
            consultation.Id
        );

        var attendance =
            await _attendanceService.CreateAsync(attendanceDto);

        entry.CreatedAttendanceId = attendance.Id;

    }  

 }