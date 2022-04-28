using Domain.Models.Enums;
using Domain.Models.Roles;
using Domain.ValueObjects;
using Domain.Models.Shopping;

namespace Domain.Models.Ordering
{
    public class Order
    {
        public int Id { get; set; }
        public Price TotalPrice { get; set; }
        public DateTime DatePlaced { get; set; }
        public OrderStatus Status { get; set; }
        public Discount Discount { get; set; }
        public UniqueCode Code { get; set; }

        //one to one
        public Buyer Buyer { get; set; } = null!;
        public int BuyerId { get; set; }

        //one to many
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}