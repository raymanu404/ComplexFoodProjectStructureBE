﻿using Microsoft.AspNetCore.Mvc;
using Application.DtoModels.Product;
using MediatR;

using Application.Features.Customer.Products.Queries.GetAllProducts;

namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: products
        [HttpGet]
        public async Task<ActionResult<IList<ProductDto>>> GetAllProducts() 
        {
            var quyerGetAllProducts = new GetAllProductsQuery();
            var products = await _mediator.Send(quyerGetAllProducts);
            if(products.Count > 0)
            {
                return Ok(products);

            }
            else
            {
                return NotFound();
            }
            
        }
        
    }
}
