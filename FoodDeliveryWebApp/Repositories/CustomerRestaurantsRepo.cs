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
        public Order CreateOrder(string sellerId, string customerId)
        {
            Order order = new Order()
            {
                TotalPrice = 0,
                DeliveryDate = DateTime.Now,
                CheckOutDate = DateTime.Now,
                SellerId = sellerId,
                CustomerId = customerId,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
        public bool UpdateOrder(Order o)
        {
            try
            {
                _context.Update(o);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public OrderProduct CreateOrderProduct(int orderId, int prodId)
        {
            OrderProduct orderProduct = new OrderProduct()
            { OrderId = orderId, ProductId = prodId };
            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
            return orderProduct;
        }
        public string GetProductSellerID(int productId)
        {
            //new ProductViewModel() { Id = p.Id, Name = p.Name, Price = p.Price, Description = p.Description, Image = p.Image };

            return _context.Products.FirstOrDefault(p => p.Id == productId)?.SellerId ?? string.Empty;
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
                              Categories = string.Join(", ", seller.Categories.Select(sc => sc.Name)),
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
                          where uRole.RoleId == roleId && seller.Categories.Any(c => categories.Contains(c))
                          select new
                          {
                              usr.Id,
                              seller.Logo,
                              Categories = string.Join(", ", seller.Categories.Select(sc => sc.Name)),
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