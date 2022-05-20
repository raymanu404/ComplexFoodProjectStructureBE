using Domain.Models.Enums;

namespace Application.DtoModels.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DatePlaced { get; set; }
        public OrderStatus Status { get; set; }
        public int Discount { get; set; }
        public string Code { get; set; }
        public int BuyerId { get; set; }

    }
}
