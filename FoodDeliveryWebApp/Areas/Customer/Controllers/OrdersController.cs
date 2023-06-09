﻿using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Repositories;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Areas.Customer.Controllers
{
    [Authorize(Roles = "Customer")]
    [Area("Customer")]
    public class OrdersController : Controller
    {
        private readonly ICustomerOrderRepo _customerOrderRepo;
        private readonly UserManager<AppUser> _userManager;

        public OrdersController(ICustomerOrderRepo customerOrderRepo, UserManager<AppUser> userManager)
        {
            _customerOrderRepo = customerOrderRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var orders = _customerOrderRepo.GetOrders(user?.Id ?? "");

            return View(orders);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = _customerOrderRepo.GetOrder(id);

            return View(order);
        }

        [HttpGet]
        public IActionResult Review(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Review(int orderId, ReviewViewModel review)
        {
            var order = _customerOrderRepo.GetOrder(orderId);

            _customerOrderRepo.AddReview(review, orderId);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _customerOrderRepo.DeleteOrderById(id);
            return RedirectToAction("Index");
        }
    }
}
