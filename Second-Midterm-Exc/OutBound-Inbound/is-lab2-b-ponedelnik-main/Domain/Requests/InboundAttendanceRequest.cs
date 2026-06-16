namespace Domain.Requests;

public record InboundAttendanceRequest(
    string UserId,
    string ConsultationId,
    DateTime AttendedAt,
    string? Notes
);


// {
// "userId": "string",
// "consultationId": "string",
// "attendedAt": "datetime",
// "notes": "string?"
// }