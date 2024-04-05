﻿using MediatR;
using Application.DtoModels.Product;

namespace Application.Features.Customer.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
