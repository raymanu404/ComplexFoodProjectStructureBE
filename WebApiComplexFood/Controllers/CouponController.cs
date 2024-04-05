using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.DtoModels.Coupon;
using Application.Features.Customer.Coupons.Commands.CreateCoupon;
using Application.Features.Customer.Coupons.Queries.GetCouponsByBuyerId;


namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("coupons")]
    public class CouponController : Controller
    {

        private readonly ILogger<CouponController> _logger;
        private readonly IMediator _mediator;

        public CouponController(IMediator mediator, ILogger<CouponController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        // GET: coupons/buyerId
        [HttpGet("{buyerId}")]
        public async Task<ActionResult<List<CouponDto>>> GetAllCouponsByBuyerId(int buyerId)
        {
            var queryGetAllCouponsByBuyerId = new GetCouponsByBuyerIdQuery() { BuyerId = buyerId };
            var coupons = await _mediator.Send(queryGetAllCouponsByBuyerId);
            if(coupons.Count > 0)
            {
                return Ok(coupons);
            }
            else
            {
                return NotFound();
            }
            
        }

        // POST: buy_coupons/buyerId
        [HttpPost("buy_coupons/{buyerId}")]
        public async Task<ActionResult<int>> CreateCoupons(int buyerId, [FromBody] CouponCreateDto newCoupons)
        {

            var command = new CreateCouponCommand
            {
                BuyerId = buyerId,
                Coupon = newCoupons
            };

            var returnMessage = await _mediator.Send(command);
            if (returnMessage.StartsWith("Successfully"))
            {
                return CreatedAtRoute(new { code = buyerId.ToString() }, returnMessage);
            }
            else
            {
                return BadRequest(returnMessage);
            }
            
        }
    
    }
}
