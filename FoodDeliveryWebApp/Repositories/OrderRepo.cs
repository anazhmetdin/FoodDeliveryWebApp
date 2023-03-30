using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Repositories
{
    public class OrderRepo : ModelRepo<Order>
    {
        public OrderRepo(FoodDeliveryWebAppContext context) : base(context)
        {
            Query = Query
                .Include(o => o.Customer.User.Addresses)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product);
        }
    }
}
