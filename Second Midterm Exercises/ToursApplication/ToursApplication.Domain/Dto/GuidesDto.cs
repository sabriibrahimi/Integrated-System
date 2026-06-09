namespace ExamsApplication.Domain.Dto;

public class GuidesDto
{
    public string UserId { get; set; }
    public Guid TourId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
