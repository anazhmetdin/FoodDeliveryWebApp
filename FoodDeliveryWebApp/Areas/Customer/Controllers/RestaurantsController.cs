using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Models.Enums;
using FoodDeliveryWebApp.Repositories;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace FoodDeliveryWebApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class RestaurantsController : Controller
    {
        private readonly ICustomerRestaurantsRepo _customerRestaurantRepo;
        private readonly IModelRepo<Category> _categoryRepo;

        public RestaurantsController(ICustomerRestaurantsRepo customerRestaurantRepo, IModelRepo<Category> categoryRepo)
        {
            _customerRestaurantRepo = customerRestaurantRepo;
            _categoryRepo = categoryRepo;
        }

        public IActionResult Index()
        {
            ICollection<SellerViewModel> sellers;
            ViewBag.Promo = ViewBag.OrderAlpha = ViewBag.OrderRate = false;

            if (Request.Query.Count > 0)
            {
                if(Request.Query.ContainsKey($"search"))
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
            Console.WriteLine(items.Count);
            return View();
        }
    }
}