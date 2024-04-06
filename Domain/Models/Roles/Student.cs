using System.ComponentModel.DataAnnotations;
using Domain.Models.Abstractions;
using Domain.Models.Enums;
using Domain.ValueObjects;

namespace Domain.Models.Roles;
public class Student : User
{
    [Key]
    public int BuyerId { get; set; }
    public string UniversityName { get; set; } = string.Empty;
    public AcademicYear AcademicYear { get; set; }
    public AcademicDegree CurrentDegree { get; set; }
    public MatrNumber MatrNumber { get; set; }

    //one to one
    public virtual Buyer Buyer { get; set; } = null!;


}
