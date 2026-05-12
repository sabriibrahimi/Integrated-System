namespace HotelApplication.Web.Response;

public record RoomWithReservationsResponse(
    Guid Id,
    int Capacity,
    int RoomNumber,
    string Status,
    double PricePerNight,
    Guid HotelId,
    string HotelName,
    List<ReservationResponse> Reservations
    );