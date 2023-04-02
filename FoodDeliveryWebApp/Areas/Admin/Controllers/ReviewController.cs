using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ReviewController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISellerRepo _sellerRepo;

        public ReviewController(UserManager<AppUser> userManager, ISellerRepo sellerRepo)
        {
            _userManager = userManager;
            _sellerRepo = sellerRepo;
        }
        public IActionResult Index(int? ratingFilter)
        {
            var sellerId = _userManager.GetUserId(User);
            var reviews = _sellerRepo.GetReviews(sellerId);

            if (ratingFilter.HasValue)
            {
                reviews = reviews.Where(r => r.Rate == ratingFilter.Value).ToList();
            }

            return View(reviews);
        }


    }
}
