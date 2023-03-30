using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class RestaurantsController : Controller
    {
        private readonly ICustomerRestaurantsRepo _customerRestaurantRepo;
        private readonly UserManager<AppUser> _userManger;
        public RestaurantsController(ICustomerRestaurantsRepo customerRestaurantRepo, UserManager<AppUser> userManager)
        {
            _customerRestaurantRepo = customerRestaurantRepo;
            _userManger = userManager;
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
            var prds = _customerRestaurantRepo.GetSellerProducts(id);
            return View(prds);
        }

        [HttpPost]
        public IActionResult Checkout([FromBody] List<CheckoutViewModel> items)
        {
            Console.WriteLine(items.Count);
            string sellerId = _customerRestaurantRepo.GetProductSellerID(items[0].Id);
            List<ProductViewModel> products = _customerRestaurantRepo.GetSellerProducts(sellerId)
                .IntersectBy(items.Select(i => i.Id), x => x.Id).ToList();
            string userId = _userManger.GetUserId(User) ?? string.Empty;
            Order o = _customerRestaurantRepo.CreateOrder(sellerId, userId);

            foreach (ProductViewModel product in products)
            {
                // update price
                o.TotalPrice += product.Price * (items
                    .FirstOrDefault(i => i.Id == product.Id)?.Count ?? 1);
                o.OrderProducts.Add(
                    _customerRestaurantRepo
                    .CreateOrderProduct(o.Id, product.Id)
                    );
                Console.WriteLine(product.Id);
            }
            _customerRestaurantRepo.UpdateOrder(o);
            return RedirectToAction("Checkout");
        }
        public IActionResult Checkout(Order order)
        {
            return View(order);
        }

    }
}