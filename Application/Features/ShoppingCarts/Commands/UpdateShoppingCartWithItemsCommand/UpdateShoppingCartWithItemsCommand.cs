using MediatR;
//using Application.DtoModels.ShoppingCartItemDto;
using Application.DtoModels.Product;

namespace Application.Features.ShoppingCarts.Commands.AddShoppingCartItemsCommand
{
    public class UpdateShoppingCartWithItemsCommand : IRequest
    {      
        public int BuyerId { get; set; }
        public ProductDto Product { get; set; }
    }

}
