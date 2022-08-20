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
        public OrderController(IMediator mediator, ILogger<OrderController> logger)
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
            if (orders.Count != 0)
            {
                return Ok(orders);
            }
            else
            {
                return NotFound();
            }
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
            if(orders.Count != 0)
            {
                return Ok(orders);
            }
            else
            {
                return NotFound();
            }
           
        }
    }

}