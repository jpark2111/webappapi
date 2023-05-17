using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
namespace WebAPI.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    public class CheckoutAPIController : ControllerBase
    {
     
        public CheckoutAPIController()
        {
            StripeConfiguration.ApiKey = "sk_test_51IzyWIALep86Y1moEj5mzhHXqAgVmXdNlGUYLJXYJQOsOzrbEHP1g2CGmLQGlVOCfMkc9iYN9wiqhUihsniRS9hD00CxMJORB5";
        }
        [HttpPost]
        public ActionResult Create()
        {
            
            var domain = "http://localhost:3000";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "USD",
                        UnitAmount = 500,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "some producgt"
                        }
                    },
                    Quantity = 1,
                    
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/success",
                CancelUrl = domain + "/cancel",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            return StatusCode(200, session.Id);
        }
    }
}
