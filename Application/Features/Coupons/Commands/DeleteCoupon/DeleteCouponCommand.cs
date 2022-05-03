using MediatR;

namespace Application.Features.Coupons.Commands.DeleteCoupon
{
    public class DeleteCouponCommand : IRequest<string>
    {
        public int BuyerId { get; set; }
        public string Code { get; set; }
    }
}
