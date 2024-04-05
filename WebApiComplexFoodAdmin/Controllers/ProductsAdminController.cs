using Application.DtoModels.Product;
using ApplicationAdmin.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApiComplexFoodAdmin.Controllers;

[ApiController]
[Route("admin/products")]
public class ProductsAdminController : Controller
{
    private readonly ILogger<ProductsAdminController> _logger;
    private readonly IMediator _mediator;

    public ProductsAdminController(ILogger<ProductsAdminController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    // GET: products
    [HttpGet]
    public async Task<ActionResult<IList<ProductDto>>> GetAllProducts()
    {
        var queryGetAllProducts = new GetAllProductsQuery();
        var products = await _mediator.Send(queryGetAllProducts);
        if (products.Count > 0)
        {
            return Ok(products);
        }

        return NotFound();
    }

    //// POST : products/create
    //[HttpPost("create")]
    //public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductDto product)
    //{
    //    var command = new CreateProductCommand
    //    {
    //        Product = product
    //    };

    //    string response = await _mediator.Send(command);
    //    if (response.Equals("Product was created successfully!")){
    //        return CreatedAtRoute(new { title = product.Title }, product);
    //    }
    //    else
    //    {
    //        return BadRequest(response);
    //    }

    //}

    ////DELETE : products/{id}
    //[HttpDelete("{productId}")]
    //public async Task<NoContentResult> DeleteProductById(int productId)
    //{
    //    var command = new DeleteProductCommand
    //    {
    //        ProductId = productId
    //    };

    //    await _mediator.Send(command);
    //    return NoContent();
    //} 

    ////PUT : products/{id}
    //[HttpPut("{productId}")]
    //public async Task<ActionResult<ProductDto>> UpdateProduct(int productId, [FromBody] ProductDto updateProduct)
    //{
    //    var command = new UpdateProductCommand
    //    {
    //        ProductId = productId,
    //        Product = updateProduct
    //    };

    //    var updateP = await _mediator.Send(command);
    //    return CreatedAtRoute(new { title = updateP.Title}, updateP);
    //}
}