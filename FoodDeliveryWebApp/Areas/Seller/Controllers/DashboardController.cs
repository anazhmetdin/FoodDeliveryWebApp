using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Areas.Seller.Controllers
{
    [Area("Seller")]
    [Authorize(Roles = "Seller")]
    [AutoValidateAntiforgeryToken]
    public class DashboardController : Controller
    {
        private readonly ISellerDashboardRepo _sellerDashboardRepo;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(ISellerDashboardRepo sellerDashboardRepo,
            UserManager<AppUser> userManager)
        {
            _sellerDashboardRepo = sellerDashboardRepo;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var sellerId = _userManager.GetUserId(User);
            return View(_sellerDashboardRepo.GetSellerDashboard(sellerId!, null));
        }

        public IActionResult Data(int? year)
        {
            var sellerId = _userManager.GetUserId(User);
            return Ok(_sellerDashboardRepo.GetSellerDashboard(sellerId!, year));
        }
    }
}
