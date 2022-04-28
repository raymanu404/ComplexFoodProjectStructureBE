using Domain.Models.Enums;
using MediatR;

namespace Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<int>
{
    public int UserId { get; set; }
    public int Amount { get; set; }
    public float TotalPrice { get; set; }
    public DateTime DatePlaced { get; set; }
    public OrderStatus Status { get; set; }
    public int Discount { get; set; }
    public string Code { get; set; } = null!;
}