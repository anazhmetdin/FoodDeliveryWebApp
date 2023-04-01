using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using FoodDeliveryWebApp.Repositories;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace FoodDeliveryWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class RestaurantsController : Controller
    {
        private readonly ICustomerRestaurantsRepo _customerRestaurantRepo;
        private readonly IModelRepo<Category> _categoryRepo;
        private readonly UserManager<AppUser> _userManger;
        public RestaurantsController(ICustomerRestaurantsRepo customerRestaurantRepo, IModelRepo<Category> categoryRepo, UserManager<AppUser> userManager)
        {
            _customerRestaurantRepo = customerRestaurantRepo;
            _categoryRepo = categoryRepo;
            _userManger = userManager;
        }

        public IActionResult Index()
        {
            ICollection<SellerViewModel> sellers;
            if (Request.Query.Count > 0)
            {
                if (Request.Query.ContainsKey($"search"))
                {
                    ViewBag.Categories = _categoryRepo.GetAll().Select(c => (c.Name, c.Id, false)).ToList();
                    sellers = _customerRestaurantRepo.GetSellersSearched(Request.Query["search"][0]).ToList();
                }
                else
                {
                    var cats = _categoryRepo.GetAll().Where(c => Request.Query.ContainsKey($"{c.Id}")).ToList();
                    sellers = _customerRestaurantRepo.GetSellersFiltered(cats);
                    ViewBag.Categories = _categoryRepo.GetAll().Select(c => (c.Name, c.Id, Request.Query.ContainsKey($"{c.Id}"))).ToList();
                }
            }
            else
            {
                ViewBag.Categories = _categoryRepo.GetAll().Select(c => (c.Name, c.Id, false)).ToList();
                sellers = _customerRestaurantRepo.GetSellers().ToList();
            }

            return View(sellers);
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
            if (items.Count < 1)
            {
                throw new ArgumentNullException("Items is Empty");
            }
            string sellerId = _customerRestaurantRepo.GetProductSellerID(items[0].Id);

            List<ProductViewModel> products = _customerRestaurantRepo.GetSellerProducts(sellerId)

                .IntersectBy(items.Select(i => i.Id), x => x.Id).ToList();

            string userId = _userManger.GetUserId(User) ?? string.Empty;

            // crazy code  start
            Order order = _customerRestaurantRepo.CreateOrder(sellerId, userId);
            // crazy code  eend



            foreach (ProductViewModel product in products)
            {
                // update price
                order.TotalPrice += product.Price * (items
                    .FirstOrDefault(i => i.Id == product.Id)?.Count ?? 1);
                order.OrderProducts.Add(
                    _customerRestaurantRepo
                    .CreateOrderProduct(order.Id, product.Id, (items
                    .FirstOrDefault(i => i.Id == product.Id)?.Count ?? 0))
                    );
                Console.WriteLine(product.Id);
            }
            _customerRestaurantRepo.UpdateOrder(order);
            return Ok(new { id = order.Id });
        }
        [HttpGet]
        public IActionResult Checkout(int id)
        {
            return View(_customerRestaurantRepo.GetOrderProduct(id));
        }
    }
}