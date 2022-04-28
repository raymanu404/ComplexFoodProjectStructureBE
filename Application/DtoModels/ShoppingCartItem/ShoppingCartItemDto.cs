using Domain.Models.Shopping;

namespace Application.DtoModels.ShoppingCartItemDto
{
    public class ShoppingCartItemDto
    {
        //public ICollection<ShoppingCartItem> Items { get; set; }
        //mai vedem aici 

        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }

    }
}
