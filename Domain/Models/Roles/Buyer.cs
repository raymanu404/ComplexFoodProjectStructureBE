using Domain.Models.Ordering;
using Domain.Models.Shopping;
using Domain.ValueObjects;

namespace Domain.Models.Roles;

public class Buyer
{
    public int Id { get; set; }
    public Email Email { get; set; }
    public Password Password { get; set; }
    public Name FirstName { get; set; } 
    public Name LastName { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public bool Confirmed { get; set; }
    public Balance Balance { get; set; }

    //one to many
    public ICollection<Coupon> Coupons { get; set; }
   

}