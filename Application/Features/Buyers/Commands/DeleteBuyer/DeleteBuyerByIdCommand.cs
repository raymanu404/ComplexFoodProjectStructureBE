using MediatR;

namespace Application.Features.Buyers.Commands.DeleteBuyer
{
    public class DeleteBuyerByIdCommand : IRequest
    {
        public int BuyerId { get; set; }
    }
}
