namespace HotelApplication.Web.Response;

public record ReservationResponse(
    Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    DateTime? ServiceDate,
    Guid RoomId,
    int? RoomNumber,
    string UserId,
    string FirstName,
    string LastName,
    Guid? HotelServiceId,
    double? ServicePrice
    );
    
    
