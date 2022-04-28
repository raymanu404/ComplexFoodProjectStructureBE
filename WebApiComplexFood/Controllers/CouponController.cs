using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using Application.DtoModels.Coupon;
using Application.Features.Coupons.Queries.GetCouponsByBuyerId;
using Application.Features.Coupons.Commands.CreateCoupon;
using Application.Features.Coupons.Commands.DeleteCoupon;
using Domain.ValueObjects;

namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CouponController : Controller
    {

        private readonly ILogger<BuyerController> _logger;
        private readonly IMediator _mediator;

        public CouponController(IMediator mediator)
        {
            //_logger = logger;
            _mediator = mediator;
        }

        // GET: CouponController/Coupons/buyerId
        [HttpGet("/Coupons/{buyerId}")]
        public async Task<ActionResult<CouponDto>> GetAllCouponsByBuyerId(int buyerId)
        {
            var queryGetAllCouponsByBuyerId = new GetCouponsByBuyerIdQuery() { BuyerId = buyerId };
            var coupons = await _mediator.Send(queryGetAllCouponsByBuyerId);
            return Ok(coupons);
        }


        // POST: CouponController/Create/buyerId
        [HttpPost("/Coupon/{buyerId}")]
        public async Task<ActionResult<CouponDto>> CreateCoupons(int buyerId, [FromBody] CouponCreateDto newCoupons)
        {

            var command = new CreateCouponCommand
            {
                BuyerId = buyerId,
                Coupon = newCoupons
            };

            var coupons = await _mediator.Send(command);
            return CreatedAtRoute(new { code = buyerId.ToString()} , coupons);
        }

        // DELETE: CouponController/id
        [HttpDelete("{buyerId}/{code}")]
        public async Task<NoContentResult> DeleteCoupon(int buyerId, string code)
        {
            var command = new DeleteCouponCommand() { BuyerId = buyerId, Code = new UniqueCode(code)};
            await _mediator.Send(command);

            return NoContent();
        }

    
    }
}
