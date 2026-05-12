namespace HotelApplication.Domain.Dto;

public class RoomReviewDto
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}