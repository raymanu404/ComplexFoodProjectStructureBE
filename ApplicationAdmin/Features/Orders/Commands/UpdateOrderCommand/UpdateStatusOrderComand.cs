using HelperLibrary.Constants;
using MediatR;

namespace ApplicationAdmin.Features.Orders.Commands.UpdateOrderCommand
{
    public class UpdateStatusOrderCommand : IRequest<StatusCodeEnum>
    {
        public int OrderId { get; set; }
        public int Status { get; set; } 
    }
}
