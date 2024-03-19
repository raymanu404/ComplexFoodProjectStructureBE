using Domain.Models.Abstractions;
using Domain.Models.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Roles;
public class Student : User
{
    public string UniversityName { get; set; } = String.Empty;
    public AcademicYear AcademicYear { get; set; }  
    public AcademicDegree CurrentDegree { get; set; }
    public MatriculationNumber MatriculationNumber { get; set; }

}
