using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using WebAPI.Models.Requests;
using WebAPI.Models.Responses;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/webhook")]
    [ApiController]
    public class StripeWebHookAPIController : ControllerBase
    {
        ILogger _logger;
        IOrderRepository _orderRepo;
        public StripeWebHookAPIController(ILogger<StripeWebHookAPIController> logger, IOrderRepository orderRepository)
        {
            _logger = logger;
            _orderRepo = orderRepository;
            StripeConfiguration.ApiKey = "sk_test_51IzyWIALep86Y1moEj5mzhHXqAgVmXdNlGUYLJXYJQOsOzrbEHP1g2CGmLQGlVOCfMkc9iYN9wiqhUihsniRS9hD00CxMJORB5";
        }

        [HttpPost]
        public async Task<ActionResult<ItemResponse<Session>>> Index()
        {
            BaseResponse response = null;
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_TePNCMugubH78tmSjjA9EGd8b8ECHyWq";
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("A successful payment for {0} was made.", paymentIntent.Amount);
                    // Then define and call a method to handle the successful payment intent.
                    // handlePaymentIntentSucceeded(paymentIntent);
                }
                else if (stripeEvent.Type == Events.PaymentMethodAttached)
                {
                    var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
                    // Then define and call a method to handle the successful attachment of a PaymentMethod.
                    // handlePaymentMethodAttached(paymentMethod);
                }
                else if(stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    OrderRequest request = new OrderRequest()
                    {
                        PaymentIntentId = session.PaymentIntentId,
                        AccountId = "Account",
                        Total = session.AmountTotal
                    };
                    _orderRepo.Add(request);
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return StatusCode(200, response);
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500, ex.Message);
            }
        }
    }
}
