using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.DtoModels.OrderItem;
using Application.Features.OrderItems.Queries.GetAllItems;
using Application.Features.OrderItems.Queries.GetALLItemsByOrderId;

namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("orderItems")]
    public class OrderItemController : Controller
    {
        private readonly ILogger<OrderItemController> _logger;
        private readonly IMediator _mediator;
        public OrderItemController(ILogger<OrderItemController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        //GET: orderItems 
        [HttpGet]
        public async Task<ActionResult<IList<OrderItemDto>>> GetAllOrderItems()
        {
            var command = new GetALLItemsQuery() { };

            var items = await _mediator.Send(command);
            return Ok(items);
        }

        //GET: /rders/{buyerId}
        [HttpGet("{orderId}")]
        public async Task<ActionResult<IList<OrderItemDto>>> GetAllOrderItemsByOrderId(int orderId)
        {
            var command = new GetALLItemsByOrderIdQuery()
            {
               OrderId = orderId
            };

            var items = await _mediator.Send(command);
            return Ok(items);
        }
    }
}
