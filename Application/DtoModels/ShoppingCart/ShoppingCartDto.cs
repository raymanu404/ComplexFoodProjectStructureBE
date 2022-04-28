using Domain.ValueObjects;

namespace Application.DtoModels.Cart
{
    public class ShoppingCartDto
    {     
        public Price TotalPrice { get; set; }
        public DateTime DatePlaced { get; set; }
        public Discount Discount { get; set; }
        public UniqueCode Code { get; set; }
        public int BuyerId { get; set; }
               
    }
}
