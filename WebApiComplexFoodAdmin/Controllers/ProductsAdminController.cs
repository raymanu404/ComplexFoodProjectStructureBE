using ApplicationAdmin.DtoModels.Product;
using ApplicationAdmin.Features.Products.Commands.CreateProduct;
using ApplicationAdmin.Features.Products.Commands.DeleteProduct;
using ApplicationAdmin.Features.Products.Commands.UpdateProduct;
using ApplicationAdmin.Features.Products.Queries.GetAllProducts;
using ApplicationAdmin.Profiles;
using HelperLibrary.Classes;
using HelperLibrary.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace WebApiComplexFoodAdmin.Controllers;



[ApiController]
[Route($"{Constants.AdminApiBase}/products")]
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
    public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery]SearchParams searchParams)
    {
        var queryGetAllProducts = new GetAllProductsQuery
        {
            SearchParams = searchParams
        };

        var products = await _mediator.Send(queryGetAllProducts);
        return Ok(products);
    }

    // POST : products/create
    [HttpPost("create")]
    public async Task<ActionResult<ProductCreateDto>> CreateProduct([FromBody] ProductCreateDto product)
    {
        var command = new CreateProductCommand
        {
            Product = product
        };

        string response = await _mediator.Send(command);
        if (response.Equals("Product was created successfully!"))
        {
            return CreatedAtRoute(new { title = product.Title }, product);
        }
        else
        {
            return BadRequest(response);
        }

    }

    //DELETE : products/{id}
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

    //PUT : products/{id}
    [HttpPut("{productId}")]
    public async Task<ActionResult<ProductDto>> UpdateProduct(int productId, [FromBody] ProductUpdateDto updateProduct)
    {
        var command = new UpdateProductCommand
        {
            ProductId = productId,
            Product = updateProduct
        };

        var updateP = await _mediator.Send(command);
        if (updateP == StatusCodeEnum.Success) return Ok();
        if (updateP == StatusCodeEnum.NotFound) return NotFound();
        return BadRequest();
    }
}