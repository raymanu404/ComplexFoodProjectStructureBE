using Domain.Models.Shopping;

namespace Application.DtoModels.ShoppingCartItemDto
{
    public class ShoppingCartItemDto
    {
        public int ProductId { get; set; }
        public int ShoppingCartId { get; set; }
        public int Amount { get; set; }

    }
}
