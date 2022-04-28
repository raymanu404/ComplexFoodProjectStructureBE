using Domain.ValueObjects;
using Domain.Models.Enums;

namespace Application.DtoModels.Coupon
{
    public class CouponCreateDto
    {
        public Amount Amount { get; set; }
        public TypeCoupons Type { get; set; }
    }
}
