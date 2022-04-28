using MediatR;
using Application.DtoModels.Coupon;
using Domain.ValueObjects;

namespace Application.Features.Coupons.Commands.CreateCoupon
{
    public class CreateCouponCommand : IRequest<List<CouponDto>> 
    { 

        public int BuyerId { get; set; }
        public CouponCreateDto Coupon { get; set; }
    }
}
