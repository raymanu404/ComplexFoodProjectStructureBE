using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Customer.ShoppingItems.Queries.GetAllProductsByCartId
{
    public class GetAllProductsByCartIdQuery : IRequest<List<ProductFromCartDto>>
    {
        public int ShoppingCartId { get; set; }
    }
}
