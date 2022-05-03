using Domain.ValueObjects;

namespace Application.DtoModels.Cart
{
    public class ShoppingCartDto
    {     
        public float TotalPrice { get; set; }
        public DateTime DatePlaced { get; set; }
        public int Discount { get; set; }
        public string Code { get; set; }
        public int BuyerId { get; set; }
               
    }
}
