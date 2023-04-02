using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Repositories
{
    public class TrendingSellerRepo : ModelRepo<TrendingSeller>, ITrendingSellerRepo
    {
        public TrendingSellerRepo(FoodDeliveryWebAppContext context) : base(context)
        {
        }

        public bool TryUpdateAll(List<string> sellers)
        {
            try
            {
                Context.TrendingSellers.Load();

                foreach (var p in Context.TrendingSellers)
                {
                    Context.Entry(p).State = EntityState.Deleted;
                }

                //Context.SaveChanges();

                Context.TrendingSellers.AddRange(sellers.Select(s => new TrendingSeller() { Id = s }));

                Context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
