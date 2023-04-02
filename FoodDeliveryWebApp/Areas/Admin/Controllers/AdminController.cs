//using FoodDeliveryWebApp.Areas.Identity.Data;
//using FoodDeliveryWebApp.Contracts;
//using FoodDeliveryWebApp.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System.Web.Http;

//namespace FoodDeliveryWebApp.Areas.Admin.Controllers
//{
//    [Area("AdminPanel")]
//    [Authorize(Roles = "Admin")]
//    public class AdminController : Controller
//    {
//        private readonly UserManager<AppUser> _userManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly ISellerRepo _sellerRepo;

//        public AdminController(
//            UserManager<AppUser> userManager,
//            RoleManager<IdentityRole> roleManager,
//            ISellerRepo sellerRepo
//            )
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _sellerRepo = sellerRepo;
//        }

//        public async Task<ActionResult> Index()
//        {
//            var ViewModel = new AdminStatsViewModel()
//            {
//                NewOrders = (await orderRepo.GetAllAsync()).Count,
//                NewProducts = (await productRepo.GetAllAsync()).Count,
//                Sellers = (await sellerRepo.GetAllAsync()).Count,
//                Customers = (await customerRepo.GetAllAsync()).Count
//            };

//            //var OrdersCount = Context.Orders.Count();
//            //ViewBag.NewOrders = OrdersCount;

//            //var ProductsCount = Context.Products.Count();
//            //ViewBag.NewProducts = ProductsCount;

//            //var SellersCount = Context.Sellers.Count();
//            //ViewBag.Sellers = SellersCount;

//            //var CustomersCount = Context.Customers.Count();
//            //ViewBag.Customers = CustomersCount;

//            //var Reports = Context.Reports.Count();
//            //ViewBag.ReportsCount = Reports;
//            return View(ViewModel);
//        }


//        public async Task<IActionResult> ManageUserRoles()
//        {
//            var users = await _userManager.Users.ToList();
//            var roles = await _roleManager.Roles.ToListAsync();

//            var viewModel = new ManageUserRolesViewModel
//            {
//                Users = users,
//                Roles = roles
//            };

//            return View(viewModel);
//        }


//        [HttpPost]
//        public async Task<IActionResult> SetUserRole(string SelectedUserId, string SelectedRoleId)
//        {
//            var user = await _userManager.FindByIdAsync(SelectedUserId);
//            var role = await _roleManager.FindByIdAsync(SelectedRoleId);

//            if (user != null && role != null)
//            {
//                // Remove user from any existing roles
//                var currentRoles = await _userManager.GetRolesAsync(user);
//                await _userManager.RemoveFromRolesAsync(user, currentRoles);

//                // Assign user to selected role
//                var result = await _userManager.AddToRoleAsync(user, role.Name);
//                if (result.Succeeded)
//                {
//                    // Role assignment was successful
//                    TempData["SuccessMessage"] = $"Role assigned successfully to user {user.UserName}.";
//                }
//                else
//                {
//                    // Handle errors
//                    TempData["ErrorMessage"] = $"An error occurred while assigning role to user {user.UserName}.";
//                }
//            }
//            else
//            {
//                TempData["ErrorMessage"] = "Invalid user or role selected.";
//            }

//            return RedirectToAction("ManageUserRoles");
//        }



//    }
//}
