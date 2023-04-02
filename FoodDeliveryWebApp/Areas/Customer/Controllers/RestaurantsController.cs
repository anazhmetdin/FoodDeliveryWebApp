using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Authorization;
using FoodDeliveryWebApp.Models.Enums;
using FoodDeliveryWebApp.Repositories;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Primitives;

namespace FoodDeliveryWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [DenySeller]
    public class RestaurantsController : Controller
    {
        private readonly ICustomerRestaurantsRepo _customerRestaurantRepo;
        private readonly IModelRepo<Category> _categoryRepo;
        private readonly UserManager<AppUser> _userManger;
        private readonly ITrendingSellerRepo _trendingSeller;
        public RestaurantsController(ICustomerRestaurantsRepo customerRestaurantRepo,
            IModelRepo<Category> categoryRepo,
            UserManager<AppUser> userManager,
            ITrendingSellerRepo trendingSeller)
        {
            _customerRestaurantRepo = customerRestaurantRepo;
            _categoryRepo = categoryRepo;
            _userManger = userManager;
            _trendingSeller = trendingSeller;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManger.GetUserAsync(User);
            if (user is not null)
            {
                var userRole = await _userManger.GetRolesAsync(user);
                if (userRole[0] == "Admin") return RedirectToAction("Index", "Users", new { area = "Admin" });
                if (userRole[0] == "Seller") return RedirectToAction("Index", "Dashboard", new { area = "Seller" });
            }
            ICollection<SellerViewModel> sellers;
            ViewBag.Promo = ViewBag.OrderAlpha = ViewBag.OrderRate = false;

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
                    var promo = Request.Query.ContainsKey("promo");
                    var orderAlpha = Request.Query.ContainsKey("name");
                    var orderRate = Request.Query.ContainsKey("rating");

                    ViewBag.Promo = promo;
                    ViewBag.OrderAlpha = orderAlpha;
                    ViewBag.OrderRate = orderRate;

                    sellers = _customerRestaurantRepo.GetSellersFiltered(cats, promo, orderAlpha, orderRate);
                    ViewBag.Categories = _categoryRepo.GetAll().Select(c => (c.Name, c.Id, Request.Query.ContainsKey($"{c.Id}"))).ToList();
                }
            }
            else
            {
                ViewBag.Categories = _categoryRepo.GetAll().Select(c => (c.Name, c.Id, false)).ToList();
                sellers = _customerRestaurantRepo.GetSellers().ToList();
            }

            #region trending sellers
            var AllTrending = _trendingSeller.GetAll();

            var trendingSellers = sellers.Where(s => AllTrending.Any(ts => ts.Id == s.Id));

            ViewBag.TrendingSellers = trendingSellers;
            #endregion

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
                OrderProduct orderProduct = _customerRestaurantRepo
                    .CreateOrderProduct(order.Id, product.Id, (items
                    .FirstOrDefault(i => i.Id == product.Id)?.Count ?? 0));
                order.OrderProducts.Add(orderProduct);
                order.TotalPrice += product.Price * (items
                    .FirstOrDefault(i => i.Id == product.Id)?.Count ?? 1);
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