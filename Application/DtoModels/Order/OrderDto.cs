using Domain.ValueObjects;
using Domain.Models.Enums;
using Application.DtoModels.Cart;

namespace Application.DtoModels.Order
{
    public class OrderDto
    {
        public string TotalPrice { get; set; }
        public DateTime DatePlaced { get; set; }
        public OrderStatus Status { get; set; }
        public int Discount { get; set; }
        public string Code { get; set; }

        //public ShoppingCartDto Cart { get; set; }
    }
}
