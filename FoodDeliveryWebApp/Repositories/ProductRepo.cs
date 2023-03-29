using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;

namespace FoodDeliveryWebApp.Repositories
{
    public class ProductRepo : ModelRepo<Product>
    {
        public ProductRepo(FoodDeliveryWebAppContext context) : base(context)
        {
        }

        public override bool TryInsert(Product t, IFormFile? Image)
        {
            CopyImage(t, Image);
            t.HasSale = t.Sale > 0;
            return TryInsert(t);
        }

        private static void CopyImage(Product t, IFormFile? Image)
        {
            if (Image == null) { return; }

            using (var stream = new MemoryStream())
            {
                Image.CopyTo(stream);
                t.Image = stream.ToArray();
            }
        }

        public override bool TryUpdate(Product t, IFormFile? Image)
        {
            CopyImage(t, Image);
            t.HasSale = t.Sale > 0;
            return TryUpdate(t);
        }
    }
}
