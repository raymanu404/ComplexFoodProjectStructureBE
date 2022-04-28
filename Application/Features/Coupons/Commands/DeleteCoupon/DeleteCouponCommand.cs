using MediatR;
using Domain.ValueObjects;

namespace Application.Features.Coupons.Commands.DeleteCoupon
{
    public class DeleteCouponCommand : IRequest<string>
    {
        public int BuyerId { get; set; }
        public UniqueCode Code { get; set; }
    }
}
