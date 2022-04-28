using Domain.Models.Enums;
using Domain.Models.Roles;
using Domain.ValueObjects;
namespace Domain.Models.Shopping;

public class Coupon
{
    public int Id { get; set; }
    public TypeCoupons Type { get; set; }
    public DateTime DateCreated { get; set; }
    public UniqueCode Code { get; set; }

    //one to many
    public Buyer Buyer { get; set; } = null!;
    public int BuyerId { get; set; }
}