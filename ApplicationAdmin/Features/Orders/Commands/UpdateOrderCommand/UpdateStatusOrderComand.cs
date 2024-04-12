using ApplicationAdmin.DtoModels.Order;
using ApplicationAdmin.Profiles;
using Domain.Models.Enums;
using HelperLibrary.Constants;
using MediatR;

namespace ApplicationAdmin.Features.Orders.Commands.UpdateOrderCommand
{
    public class UpdateStatusOrderCommand : IRequest<StatusCodeEnum>
    {
        public int BuyerId { get; set; }
        public int Status { get; set; } 
    }
}
