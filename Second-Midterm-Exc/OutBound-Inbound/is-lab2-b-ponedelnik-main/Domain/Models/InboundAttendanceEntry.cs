using Domain.Enums;

namespace Domain.Models;

public class InboundAttendanceEntry
{
    public Guid Id { get; set; }

    public string? RawPayload { get; set; }
    public ProcessingStatus Status { get; set; }

    public Guid ApiClientId { get; set; }
    public virtual ApiClient ApiClient { get; set; } = null!;

    public DateTime ReceivedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }

    public string? ErrorMessage { get; set; }
    public Guid? CreatedAttendanceId { get; set; }
}