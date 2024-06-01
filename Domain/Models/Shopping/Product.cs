using Domain.Models.Enums;
using Domain.ValueObjects;
using Domain.Models.Ordering;

namespace Domain.Models.Shopping
{

    public class Product
    {
        public int Id { get; set; }
        public Categories Category { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public Price Price { get; set; }
        public string Image { get; set; } = null!;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public bool IsInStock { get; set; }
        public Price MerchantPrice { get; set; }

        //one to many
        public ICollection<OrderItem> OrderItems { get; set; } = null!;
    }
}