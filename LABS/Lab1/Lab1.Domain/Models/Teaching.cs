using Lab1.Domain.Common;
using Lab1.Domain.Enums;

namespace Lab1.Domain.Models;

public class Teaching : BaseEntity
{
   public  Role Role { get; set; }
   
   public Guid CourseId { get; set; }
   public virtual Course Course { get; set; } = null!;
   
   public Guid SemesterId { get; set; }
   public virtual Semester Semester { get; set; } = null!;

   public string UserId { get; set; } = null!;
   public virtual CoursesApplicationUser User { get; set; } = null!;
}