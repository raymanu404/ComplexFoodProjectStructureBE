using Domain.ValueObjects;
using Domain.Models.Enums;
using Application.DtoModels.Cart;

namespace Application.DtoModels.Order
{
    public class OrderDto
    {
        public Price TotalPrice { get; set; }
        public DateTime DatePlaced { get; set; }
        public OrderStatus Status { get; set; }
        public Discount Discount { get; set; }
        public UniqueCode Code { get; set; }

        public ShoppingCartDto Cart { get; set; }
    }
}
