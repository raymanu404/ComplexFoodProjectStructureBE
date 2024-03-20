using Domain.Models.Abstractions;
using Domain.Models.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Roles;
public class Student : User
{
    [Key]
    public int BuyerId { get; set; }
    public string UniversityName { get; set; } = String.Empty;
    public AcademicYear AcademicYear { get; set; }  
    public AcademicDegree CurrentDegree { get; set; }
    public MatrNumber MatrNumber { get; set; }

    //one to one
    public virtual Buyer Buyer { get; set; } = null!;


}
