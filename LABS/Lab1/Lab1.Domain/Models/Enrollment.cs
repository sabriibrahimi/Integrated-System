using System.Runtime.InteropServices.JavaScript;
using Lab1.Domain.Common;

namespace Lab1.Domain.Models;

public class Enrollment : BaseEntity
{
    public DateTime EnrolledAt { get; set; }

    public string UserId { get; set; } = null!;
    public virtual CoursesApplicationUser User { get; set; } = null!;
    
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;
}