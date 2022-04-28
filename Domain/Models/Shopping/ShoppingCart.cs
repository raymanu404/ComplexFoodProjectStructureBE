using Domain.ValueObjects;
using Domain.Models.Roles;

namespace Domain.Models.Shopping
{
    public class ShoppingCart
    {
        public int Id { get; set; }      
        public Price TotalPrice { get; set; }
        public DateTime DatePlaced { get; set; }
        public Discount Discount { get; set; }
        public UniqueCode Code { get; set; }

        //one to one
        public Buyer Buyer { get; set; }
        public int BuyerId { get; set; }
        
        //one to many 
        public ICollection<ShoppingCartItem> Items { get; set; }
       
    }
}