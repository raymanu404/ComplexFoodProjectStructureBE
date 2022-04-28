using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DtoModels.Product;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Products.Queries.GetProductById;
using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using MediatR;

namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;

        public ProductController(ILogger<ProductController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: Products
        [HttpGet("Products")]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts() 
        {
            var quyerGetAllProducts = new GetAllProductsQuery();
            var products = await _mediator.Send(quyerGetAllProducts);
            return Ok(products);
        }

        //GET : Product/{productId}
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int productId)
        {
            var queryGetProductById = new GetProductByIdQuery
            {
                ProductId = productId
            };
            var product = await _mediator.Send(queryGetProductById);
            return Ok(product);
        }

        // POST : Product
        [HttpPost("/create-product")]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductDto product)
        {
            var command = new CreateProductCommand
            {
                Product = product
            };
            await _mediator.Send(command);
            return CreatedAtRoute(new { title = product.Title }, product);
        }

        //DELETE : Product/{id}
        [HttpDelete("{productId}")]
        public async Task<NoContentResult> DeleteProductById(int productId)
        {
            var command = new DeleteProductCommand
            {
                ProductId = productId
            };

            await _mediator.Send(command);
            return NoContent();
        } 

        //PUT : Product/{id}
        [HttpPut("{productId}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int productId, [FromBody] ProductDto updateProduct)
        {
            var command = new UpdateProductCommand
            {
                ProductId = productId,
                Product = updateProduct
            };

            var updateP = await _mediator.Send(command);
            return CreatedAtRoute(new { title = updateP.Title}, updateP);
        }
        
    }
}
