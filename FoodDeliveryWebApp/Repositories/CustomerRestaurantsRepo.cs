using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Repositories
{
    public class CustomerRestaurantsRepo : ICustomerRestaurantsRepo
    {
        private readonly FoodDeliveryWebAppContext _context;

        public CustomerRestaurantsRepo(FoodDeliveryWebAppContext context)
        {
            _context = context;
        }

        public ICollection<ProductViewModel> GetSellerProducts(string sellerId)
        {
            if (string.IsNullOrWhiteSpace(sellerId))
            {
                throw new ArgumentNullException(nameof(sellerId));
            }

            return _context.Products.Where(p => p.SellerId == sellerId && p.InStock)
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Image = $"data:image/png;base64,{Convert.ToBase64String(p.Image)}"
                }).ToList();
        }

        public ICollection<AppUser> GetSellers()
        {
            var roleId = _context.Roles.Where(r => r.Name == "Seller").Select(s => s.Id).FirstOrDefault();

            var sellers = from usr in _context.Users
                          join uRole in _context.UserRoles
                          on usr.Id equals uRole.UserId
                          where uRole.RoleId == roleId
                          select usr.Id;

            List<AppUser> users = new();
            foreach (var id in sellers)
            {
                var res = _context.Users.Find(id);
                if (res != null) users.Add(res);
            }

            return users;
        }

        public ICollection<AppUser> GetSellersFiltered(Func<AppUser, bool> func)
        {
            var sellers = GetSellers();

            return sellers.Where(func).ToList();
        }
    }
}