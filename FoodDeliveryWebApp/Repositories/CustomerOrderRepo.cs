using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using FoodDeliveryWebApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Repositories
{
    public class CustomerOrderRepo : ICustomerOrderRepo
    {
        private readonly FoodDeliveryWebAppContext _context;

        public CustomerOrderRepo(FoodDeliveryWebAppContext context)
        {
            _context = context;
        }
        public bool DeleteOrderById(int orderId)
        {
            try
            {
                Order order = _context.Orders.FirstOrDefault(o => o.Id == orderId);
                _context.Remove(order);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }

        public bool AddReview(ReviewViewModel review, int orderId)
        {
            try
            {
                var order = _context.Orders.Find(orderId);
                order.Review = new()
                {
                    CustomerId = order.CustomerId,
                    SellerId = order.SellerId,
                    Rate = review.Rate,
                    UserReview = review.UserReview
                };

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public OrderViewModel GetOrder(int id)
        {
            return _context.Orders.Where(o => o.Id == id)
                .Include(o => o.OrderProducts).ThenInclude(op => op.Product).ToList()
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    CheckOutDate = o.CheckOutDate,
                    DeliveryDate = o.DeliveryDate,
                    PromoCode = _context.PromoCodes.Find(o.PromoCodeId),
                    Seller = _context.Sellers.Find(o.SellerId),
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Review = _context.Reviews.Find(o.ReviewId),
                    Products = o.OrderProducts.Select(op => new ProductViewModel()
                    {
                        Id = op.Product.Id,
                        Name = op.Product.Name,
                        Description = op.Product.Description,
                        Image = $"data:image/png;base64,{Convert.ToBase64String(op.Product.Image)}",
                        Price = op.UnitPrice,
                        Quantity = op.Quantity
                    }).ToList()
                }).First();
        }

        public ICollection<OrderViewModel> GetOrders(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _context.Orders.Where(o => o.CustomerId == id).Include(o => o.OrderProducts).ThenInclude(op => op.Product).ToList()
                .Select(o => new OrderViewModel()
                {
                    Id = o.Id,
                    CheckOutDate = o.CheckOutDate,
                    DeliveryDate = o.DeliveryDate,
                    PromoCode = _context.PromoCodes.Find(o.PromoCodeId),
                    Seller = _context.Sellers.Find(o.SellerId),
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Review = _context.Reviews.Find(o.ReviewId),
                    Products = o.OrderProducts.Select(op => new ProductViewModel()
                    {
                        Id = op.Product.Id,
                        Name = op.Product.Name,
                        Description = op.Product.Description,
                        Image = $"data:image/png;base64,{Convert.ToBase64String(op.Product.Image)}",
                        Price = op.UnitPrice,
                        Quantity = op.Quantity
                    }).ToList()
                }).ToList();
        }
    }
}
