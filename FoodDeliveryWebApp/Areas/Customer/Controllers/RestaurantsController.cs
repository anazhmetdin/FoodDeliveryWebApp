using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class RestaurantsController : Controller
    {
        private readonly ICustomerRestaurantsRepo _customerRestaurantRepo;

        public RestaurantsController(ICustomerRestaurantsRepo customerRestaurantRepo)
        {
            _customerRestaurantRepo = customerRestaurantRepo;
        }

        public IActionResult Index()
        {
            return View(_customerRestaurantRepo.GetSellers());
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            return RedirectToAction("Restaurant", new { id });
        }


        [HttpGet]
        public IActionResult Restaurant(string id)
        {
            var prds =  _customerRestaurantRepo.GetSellerProducts(id);
            return View(prds);
        }

        [HttpPost]
        public IActionResult Checkout([FromBody] List<CheckoutViewModel> items)
        {
            Console.WriteLine(items.Count);
            return View();
        }

    }
}