using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.ShoppingItems.Queries.GetAllProductsByBuyerId
{
    public class GetAllProductsByBuyerIdQuery : IRequest<List<ProductFromCartDto>>
    {
        public int BuyerId { get; set; }
    }
}
