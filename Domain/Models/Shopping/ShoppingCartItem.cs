using Domain.ValueObjects;

namespace Domain.Models.Shopping
{
    public class ShoppingCartItem
    {
        
        //one to many
        public ShoppingCart ShoppingCart { get; set; }
        public int ShoppingCartId { get; set; }

        //one to one
        public Product Product { get; set; }    
        public int ProductId { get; set; }

        public Amount Amount { get; set; }

    }
}
