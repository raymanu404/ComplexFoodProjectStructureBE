using Domain.ValueObjects;
using Domain.Models.Enums;
using Application.DtoModels.Buyer;

namespace Application.DtoModels.Coupon
{
    public class CouponDto
    {
        public TypeCoupons Type { get; set; }
        public DateTime DateCreated { get; set; }
        public UniqueCode Code { get; set; }
        public int BuyerId { get; set; }
    }
}
