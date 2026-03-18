using Lab1.Domain.Common;

namespace Lab1.Domain.Models;

public class Semester  : BaseEntity
{
     public required string Name { get; set; }
     public DateTime StartDate { get; set; }
     public DateTime EndDate { get; set; }
     
     public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();
     public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

}