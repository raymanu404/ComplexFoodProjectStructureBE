using Domain.ValueObjects;

namespace Domain.Models.Shopping
{
    public class ShoppingCartItem
    {

        //one to many
        public ShoppingCart ShoppingCart { get; set; }
        public int ShoppingCartId { get; set; }

        //one to many
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Cantity Cantity { get; set; }

    }
}
