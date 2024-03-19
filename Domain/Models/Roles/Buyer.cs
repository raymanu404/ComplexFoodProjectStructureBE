using Domain.Models.Abstractions;
using Domain.Models.Enums;
using Domain.Models.Ordering;
using Domain.Models.Shopping;
using Domain.ValueObjects;

namespace Domain.Models.Roles;

public class Buyer : User
{
    public PhoneNumber PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public bool Confirmed { get; set; }
    public Balance Balance { get; set; }
    public UniqueCode ConfirmationCode { get; set; }
    //adaugam rolul in plus, daca un user are rol de student, vom avea referinta dupa email
    public RoleNameEnum Role { get; set; } = RoleNameEnum.User;

    //one to many
    public ICollection<Coupon> Coupons { get; set; }
   

}