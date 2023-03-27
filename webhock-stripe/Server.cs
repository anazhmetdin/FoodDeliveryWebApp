using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Stripe;

namespace StripeExample
{
  public class Program
  {
    public static void Main(string[] args)
    {
      WebHost.CreateDefaultBuilder(args)
        .UseUrls("http://0.0.0.0:4242")
        .UseWebRoot("public")
        .UseStartup<Startup>()
        .Build()
        .Run();
    }
  }

  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().AddNewtonsoftJson();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // This is a public sample test API key.
      // Don’t submit any personally identifiable information in requests made with this key.
      // Sign in to see your own test API key embedded in code samples.
      StripeConfiguration.ApiKey = "sk_test_26PHem9AhJZvU623DfE1x4sd";

      if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
      app.UseRouting();
      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }

  [Route("webhook")]
  [ApiController]
  public class WebhookController : Controller
  {
    [HttpPost]
    public async Task<IActionResult> Index()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        const string endpointSecret = "whsec_...";
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
}