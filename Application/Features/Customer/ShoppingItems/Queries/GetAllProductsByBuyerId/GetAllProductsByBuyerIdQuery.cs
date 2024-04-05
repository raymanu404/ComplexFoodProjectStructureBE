using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Customer.ShoppingItems.Queries.GetAllProductsByBuyerId
{
    public class GetAllProductsByBuyerIdQuery : IRequest<List<ProductFromCartDto>>
    {
        public int BuyerId { get; set; }
    }
}
