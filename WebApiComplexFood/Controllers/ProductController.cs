using Microsoft.AspNetCore.Mvc;
using Application.DtoModels.Product;
using Application.Features.Products.Queries.GetAllProducts;
using MediatR;


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
            return Ok(products);

        }
        
    }
}
