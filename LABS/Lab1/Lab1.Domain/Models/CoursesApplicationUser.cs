namespace Lab1.Domain.Models;
using Microsoft.AspNetCore.Identity;


public class CoursesApplicationUser : IdentityUser
{
     public required string FirstName { get; set; } 
     public required string LastName { get; set; }
     public DateTime DateOfBirth { get; set; }
     
     public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
     public virtual ICollection<Teaching> Teachings { get; set; } = new List<Teaching>();


}