using MediatR;
using Domain.ValueObjects;

namespace Application.Features.OrderItems.Commands.UpdateItemCommand
{
    public class UpdateItemCommand : IRequest
    {
        public int ItemId { get; set; }
        public int Amount { get; set; }
        
        //mai vedem daca actualizam si produsul in sine
        public int ProductId { get; set; }
    }
}
