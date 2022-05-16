using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using Application.DtoModels.Coupon;
using Application.Features.Coupons.Queries.GetCouponsByBuyerId;
using Application.Features.Coupons.Commands.CreateCoupon;
using Application.Features.Coupons.Commands.DeleteCoupon;

namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("coupons")]
    public class CouponController : Controller
    {

        private readonly ILogger<BuyerController> _logger;
        private readonly IMediator _mediator;

        public CouponController(IMediator mediator)
        {
            //_logger = logger;
            _mediator = mediator;
        }

        // GET: coupons/buyerId
        [HttpGet("{buyerId}")]
        public async Task<ActionResult<CouponDto>> GetAllCouponsByBuyerId(int buyerId)
        {
            var queryGetAllCouponsByBuyerId = new GetCouponsByBuyerIdQuery() { BuyerId = buyerId };
            var coupons = await _mediator.Send(queryGetAllCouponsByBuyerId);
            return Ok(coupons);
        }


        // POST: buy_coupons/buyerId
        [HttpPost("buy_coupons/{buyerId}")]
        public async Task<ActionResult<NoContentResult>> CreateCoupons(int buyerId, [FromBody] CouponCreateDto newCoupons)
        {

            var command = new CreateCouponCommand
            {
                BuyerId = buyerId,
                Coupon = newCoupons
            };

            var coupons = await _mediator.Send(command);
            return CreatedAtRoute(new { code = buyerId.ToString()} , coupons);
        }

        // DELETE: coupons/{buyerId}/{code}
        [HttpDelete("{buyerId}/{code}")]
        public async Task<NoContentResult> DeleteCoupon(int buyerId, string code)
        {
            var command = new DeleteCouponCommand() { BuyerId = buyerId, Code = code};
            await _mediator.Send(command);

            return NoContent();
        }

    
    }
}
