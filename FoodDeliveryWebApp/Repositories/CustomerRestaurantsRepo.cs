using FoodDeliveryWebApp.Areas.Identity.Data;
using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public ICollection<SellerViewModel> GetSellers()
        {
            var roleId = _context.Roles.Where(r => r.Name == "Seller").Select(s => s.Id).FirstOrDefault();

            var sellers = from usr in _context.Users
                          join uRole in _context.UserRoles
                          on usr.Id equals uRole.UserId
                          join seller in _context.Sellers
                          on usr.Id equals seller.Id
                          where uRole.RoleId == roleId
                          select new 
                          { 
                              usr.Id,
                              seller.Logo,
                              Categories = string.Join(", ", seller.SellerCategories.Select(sc => sc.Category.Name)),
                              seller.StoreName 
                          };

            List<SellerViewModel> restaurants = new();

            foreach (var seller in sellers)
            {
                restaurants.Add(new()
                {
                    Id = seller.Id,
                    Categories = string.Join(", ", seller.Categories),
                    StoreName = seller.StoreName,
                    Logo = $"data:image/png;base64,{Convert.ToBase64String(seller.Logo)}"
                });
            }

            return restaurants;
        }

        public ICollection<SellerViewModel> GetSellersFiltered(List<Category> categories)
        {
            var roleId = _context.Roles.Where(r => r.Name == "Seller").Select(s => s.Id).FirstOrDefault();

            var sellers = from usr in _context.Users
                          join uRole in _context.UserRoles
                          on usr.Id equals uRole.UserId
                          join seller in _context.Sellers
                          on usr.Id equals seller.Id
                          where uRole.RoleId == roleId && seller.SellerCategories.Any(c => categories.Contains(c.Category))
                          select new
                          {
                              usr.Id,
                              seller.Logo,
                              Categories = string.Join(", ", seller.SellerCategories.Select(sc => sc.Category.Name)),
                              seller.StoreName
                          };

            List<SellerViewModel> restaurants = new();

            foreach (var seller in sellers)
            {
                restaurants.Add(new()
                {
                    Id = seller.Id,
                    Categories = string.Join(", ", seller.Categories),
                    StoreName = seller.StoreName,
                    Logo = $"data:image/png;base64,{Convert.ToBase64String(seller.Logo)}"
                });
            }

            return restaurants;
        }
    }
}