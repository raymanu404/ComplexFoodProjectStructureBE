using MediatR;

namespace Application.Features.Buyers.Commands.DeleteBuyer
{
    public class DeleteBuyerByIdCommand : IRequest<Unit>
    {
        public int BuyerId { get; set; }
    }
}
