using System.Text.Json.Serialization;

namespace Domain.ConsultationApiResponse;

public class ConsultationApiResponse
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("startTime")] public DateTime StartTime { get; set; }

    [JsonPropertyName("endTime")] public DateTime EndTime { get; set; }

    [JsonPropertyName("roomId")] public Guid RoomId { get; set; }

    [JsonPropertyName("roomName")] public string RoomName { get; set; } = string.Empty;

    [JsonPropertyName("comments")] public List<ConsultationCommentResponse> Comments { get; set; } = new();
}

public class ConsultationCommentResponse
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("comment")] public string Comment { get; set; } = string.Empty;
}