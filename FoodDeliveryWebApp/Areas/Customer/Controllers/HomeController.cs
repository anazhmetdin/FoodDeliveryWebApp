using FoodDeliveryWebApp.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerHomeRepo _customerHomeRepo;

        public HomeController(ICustomerHomeRepo customerHomeRepo)
        {
            _customerHomeRepo = customerHomeRepo;
        }

        public IActionResult Index()
        {
            return View(_customerHomeRepo.GetSellers());
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            return RedirectToAction("Index", "Seller", new { id });
        }
    }
}
