using CoursesApplication.Domain.Common;
using CoursesApplication.Domain.Dto;
using CoursesApplication.Web.Response;

namespace CoursesApplication.Domain.Models;

public class ExamAttempt : BaseEntity
{
    public Guid ExamId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }

   
}