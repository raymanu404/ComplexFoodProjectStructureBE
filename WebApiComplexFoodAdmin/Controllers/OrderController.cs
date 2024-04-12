using ApplicationAdmin.DtoModels.Order;
using ApplicationAdmin.Features.Orders.Commands.UpdateOrderCommand;
using ApplicationAdmin.Features.Orders.Queries.GetAllOrders;
using ApplicationAdmin.Features.Orders.Queries.GetOrderByBuyerId;
using ApplicationAdmin.Features.Orders.Queries.GetOrderById;
using ApplicationAdmin.Features.Orders.Queries.GetOrdersByBuyer;
using HelperLibrary.Classes;
using HelperLibrary.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApiComplexFoodAdmin.Controllers
{
    [ApiController]
    [Route($"{Constants.AdminApiBase}/orders")]
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
        public async Task<ActionResult<IList<OrderDto>>> GetAllOrders([FromQuery] SearchParams searchParams)
        {
            var command = new GetAllOrdersQuery
            {
                SearchParams = searchParams
            };

            var orders = await _mediator.Send(command);
            return Ok(orders);
        }

        [HttpGet("buyer-order/{buyerId}")]
        public async Task<ActionResult<OrderDto>> GetOrderByBuyerId(int buyerId)
        {
            var command = new GetOrderByBuyerIdQuery() 
            {
                BuyerId = buyerId
            };

            var order = await _mediator.Send(command);
            return Ok(order);

        }

        [HttpGet("buyer-orders/{buyerId}")]
        public async Task<ActionResult<IList<OrderDto>>> GetAllBuyerOrders(int buyerId, [FromQuery] SearchParams searchParams)
        {
            var command = new GetOrdersByBuyerIdQuery
            {
                SearchParams = searchParams,
                BuyerId = buyerId
            };

            var orders = await _mediator.Send(command);
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
        {
            var command = new GetOrderByIdQuery()
            {
                OrderId = orderId
            };

            var order = await _mediator.Send(command);
            return Ok(order);

        }

        [HttpPut("update-status/{buyerId}")]
        public async Task<ActionResult<OrderDto>> UpdateStatusOrderByBuyerId(int buyerId, [FromQuery] int orderStatus)
        {
            var command = new UpdateStatusOrderCommand()
            {
              BuyerId = buyerId,
              Status = orderStatus
            };

            var response = await _mediator.Send(command);
            if (response == StatusCodeEnum.Success) return Ok(response);
            if (response == StatusCodeEnum.NoOperation) return Ok(response);
            if (response == StatusCodeEnum.NotFound) return Ok(response);

            return BadRequest();

        }
    }

}