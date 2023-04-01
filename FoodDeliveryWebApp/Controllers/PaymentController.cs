using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Repositories;
using Microsoft.AspNet.WebHooks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace FoodDeliveryWebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ICustomerRestaurantsRepo _customerRestaurantRepo;

        public PaymentController(ICustomerRestaurantsRepo customerRestaurantRepo)
        {
            _customerRestaurantRepo = customerRestaurantRepo;
        }
        // GET: PaymentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PaymentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentController/Create
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        // POST: PaymentController/Create
        [HttpPost]
        public ActionResult Create([FromBody] TotalPrice items)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = items.total_price,
                Currency = "usd",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });
            Order o = _customerRestaurantRepo.GetOrder(items.order_id);
            o.Payment = new Payment()
            {
                StripeId = paymentIntent.Id,
                Amount = paymentIntent.Amount,
                AmountReceived = paymentIntent.AmountReceived
            };
            _customerRestaurantRepo.UpdateOrder(o);
            return Json(new { clientSecret = paymentIntent.ClientSecret });

        }

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            const string endpointSecret = "whsec_26839dec5f0f476ef9b48676d3fb0fbff6953e35133eb5e20b6eab9d9e310e54";
            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var signatureHeader = Request.Headers["Stripe-Signature"];

                stripeEvent = EventUtility.ConstructEvent(json,
                        signatureHeader, endpointSecret);
                await Console.Out.WriteLineAsync(stripeEvent.Type);

                if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                    Console.WriteLine("A successful payment for {0} was made.", paymentIntent.Amount);

                    // Then define and call a method to handle the successful payment intent.
                    // handlePaymentIntentSucceeded(paymentIntent);
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
    public class TotalPrice
    {
        public long total_price { get; set; }
        public int order_id { get; set; }
    }
}
