using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Application.Models;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using Application.DtoModels.PaymentDto;

namespace WebApiComplexFood.Controllers
{
    [ApiController]
    [Route("payment")]
    public class PaymentItentController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;
        private readonly StripeSettings _stripeSettings;

        public PaymentItentController(ILogger<ProductController> logger, IMediator mediator, IOptions<StripeSettings> stripeSettings)
        {
            _logger = logger;
            _mediator = mediator;
            _stripeSettings = stripeSettings.Value;

        }

        [HttpGet("publishableKey")]
        public ActionResult<string> GetPublishableKey()
        {         
            return Ok(_stripeSettings.PublishableKey);
        }

        [HttpPost("create-payment-intent")]
        public async Task<IActionResult> CreatePaymentIntent([FromBody] CustomerDto customerDto)
        {

            try
            {
                var customerService = new CustomerService();
                var chargerService = new ChargeService();
                var cardService = new CardService();
                var paymentIntentService = new PaymentIntentService();

                var customerOptions = new CustomerCreateOptions
                {
                    Name = customerDto.FullName,                  
                    Email = customerDto.Email,
                    Address = new AddressOptions
                    {
                        City = customerDto.City,
                        Country = customerDto.Country,
                        Line1 = customerDto.AddressLine1,
                        //PostalCode = customerDto.PostalCode
                    },
                    Source = "tok_mastercard",                                  
                    Phone = customerDto.Phone,
                    
                };

                var cardOptions = new CardCreateOptions
                {
                    Source = customerOptions.Source,
                };



                var customer = await customerService.CreateAsync(customerOptions);

                var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
                {
                    Amount = customerDto.Amount * 100,
                    Currency = "ron",
                    PaymentMethodTypes = new List<string> { "card" },
                    Customer = customer.Id,
                    SetupFutureUsage = "off_session",

                });

                var returnObj = new
                {
                    clientSecret = paymentIntent.ClientSecret,
                    customer = customer.Id,
                };

                return Ok(returnObj);

            }catch(StripeException error)
            {
                return BadRequest(new
                {
                    Error = new
                    {
                        Message = error.Message
                    }
                });
            
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
