using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Repositories
{
    public class CustomerHomeRepo : ICustomerHomeRepo
    {
        private readonly FoodDeliveryWebAppContext _context;

        public CustomerHomeRepo(FoodDeliveryWebAppContext context)
        {
            _context = context;
        }

        public ICollection<Product> GetSellerProducts(string sellerId)
        {
            if (string.IsNullOrWhiteSpace(sellerId))
            {
                throw new ArgumentNullException(nameof(sellerId));
            }

            return _context.Products.Where(p => p.SellerId == sellerId).ToList();
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
                if(res != null)  users.Add(res);
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
