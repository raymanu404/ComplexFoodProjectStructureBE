using MediatR;

namespace Application.Features.Customer.Buyers.Commands.DeleteBuyer
{
    public class DeleteBuyerByIdCommand : IRequest
    {
        public int BuyerId { get; set; }
    }
}
