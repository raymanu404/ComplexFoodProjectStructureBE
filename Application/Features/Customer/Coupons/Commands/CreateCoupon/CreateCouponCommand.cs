using MediatR;
using Application.DtoModels.Coupon;
using Domain.ValueObjects;

namespace Application.Features.Customer.Coupons.Commands.CreateCoupon
{
    public class CreateCouponCommand : IRequest<string>
    {

        public int BuyerId { get; set; }
        public CouponCreateDto Coupon { get; set; }
    }
}
