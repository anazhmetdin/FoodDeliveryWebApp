using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDeliveryWebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PromoCodeController : Controller
    {
        
        private readonly IPromoCodeRepo _repo;

        public PromoCodeController(IPromoCodeRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var promocodes = _repo.GetAll();
            
            return View(promocodes);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Add(PromoCode code) 
        { 
            _repo.TryInsert(code);
            return Redirect("/promocode/index");
        }
        public IActionResult Edit(int id)
        {
            var code = _repo.GetById(id);
            return View(code);
        }
        public IActionResult SubmitEdit(PromoCode code)
        {
            _repo.TryUpdate(code);
            return Redirect("/promocode/index");
        }
        public IActionResult Delete(int id)
        {
            _repo.TryDelete(id);
            return Redirect("/promocode/index");
        }
    }
}
