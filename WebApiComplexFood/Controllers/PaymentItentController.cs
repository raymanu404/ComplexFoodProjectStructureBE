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

                //var cardNestedOptions = new CardCreateNestedOptions
                //{

                //    AddressCity = customerOptions.Address.City,
                //    AddressState = customerOptions.Address.State,
                //    AddressCountry = customerOptions.Address.Country,
                //    AddressLine1 = customerOptions.Address.Line1,

                //};

                var customer = await customerService.CreateAsync(customerOptions);

                //var card = await cardService.CreateAsync(customer.Id, cardOptions);

                //var chargeOptions = new ChargeCreateOptions
                //{
                //    Amount = paymentIntentDto.Amount * 100,
                //    Currency = "ron",
                //    Customer = customer.Id,
                //    Source = customerOptions.Source,

                //};


                //var charge = await chargerService.CreateAsync(chargeOptions);

                var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
                {
                    Amount = customerDto.Amount * 100,
                    Currency = "ron",
                    PaymentMethodTypes = new List<string> { "card" },
                    Customer = customer.Id,
                    SetupFutureUsage = "off_session",
                   
                    //AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions { Enabled  = true},
                    //PaymentMethod = card.Id,
                    //UseStripeSdk = true,
                    //ConfirmationMethod = "automatic",

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

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("PaymentIntent was successful!");
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    Console.WriteLine("PaymentMethod was attached to a Customer!");
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest();
            }
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            try
            {
                var domain = "http://localhost:4242";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>
                   
                {
                  new SessionLineItemOptions
                  {

                    Price = "price_1L4QNwGSnWTvEvr5XrehHxqb",
                    Quantity = 1,
                    
                  },
                },
                    Mode = "subscription",
                    PaymentMethodTypes = new List<string>
                    {
                        "card"
                    },
                    SuccessUrl = domain + "/success.html",
                    CancelUrl = domain + "/cancel.html",
                };
                var service = new SessionService();
                Session session = await  service.CreateAsync(options);
                return Ok(new
                {
                    SessionId = session.Id
                });

            }
            catch(StripeException error)
            {
                return BadRequest(new
                {
                    Error = new
                    {
                        Message = error.Message
                    }
                });
            }catch(Exception exception)
            {
                return BadRequest();
            }
        }
    }
}
