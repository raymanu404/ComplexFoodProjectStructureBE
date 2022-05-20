using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.DtoModels.Order;
using Application.Features.Orders.Queries.GetAllOrders;
using Application.Features.Orders.Queries.GetOrdersByBuyer;

namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;
        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        //GET: orders 
        [HttpGet]
        public async Task<ActionResult<IList<OrderDto>>> GetAllOrders()
        {
            var command = new GetAllOrdersQuery() { };

            var orders = await _mediator.Send(command);
            return Ok(orders);
        } 

        //GET: orders/{buyerId}
        [HttpGet("{buyerId}")]
        public async Task<ActionResult<IList<OrderDto>>> GetAllOrdersByBuyerId(int buyerId)
        {
            var command = new GetOrdersByBuyerIdQuery() 
            {
                BuyerId = buyerId
            };

            var orders = await _mediator.Send(command);
            return Ok(orders);
        }
    }

}