using MediatR;
using Application.DtoModels.Coupon;

namespace Application.Features.Customer.Coupons.Queries.GetCouponsByBuyerId
{
    public class GetCouponsByBuyerIdQuery : IRequest<List<CouponDto>>
    {
        public int BuyerId { get; set; }
    }
}
