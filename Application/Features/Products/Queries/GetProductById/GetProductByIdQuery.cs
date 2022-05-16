using MediatR;
using Application.DtoModels.Product;
using Domain.Models.Shopping;

namespace Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int ProductId { get; set; }
    }
}
