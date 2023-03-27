using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryWebApp.Repositories
{
    public class ModelRepo<T> : IModelRepo<T> where T : BaseModel
    {
        public ModelRepo(FoodDeliveryWebAppContext context)
        {
            Context = context;
        }
        public FoodDeliveryWebAppContext Context { get; }

        public virtual List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public virtual T? GetById(int? id)
        {
            return Context.Set<T>().Find(id);
        }

        public virtual bool TryDelete(int? id)
        {
            if (GetById(id) is T t && t != null)
            {
                Context.Set<T>().Remove(t);
                Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual List<T> Where(Func<T, bool> lambda)
        {
            return Context.Set<T>().Where(lambda).ToList();
        }

        public bool TryInsert(T t)
        {
            try
            {
                Context.Set<T>().Add(t);
                Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool TryInsert(Product t, IFormFile Image) { return false; }
        public virtual bool TryUpdate(Product t, IFormFile Image) { return false; }

        public virtual bool TryUpdate(T t)
        {
            try
            {
                if (GetById(t.Id) == null)
                {
                    throw new KeyNotFoundException();
                }

                var local = Context.Set<T>().Local.FirstOrDefault(s => s.Id == t.Id);
                if (local != null)
                    Context.Entry(local).State = EntityState.Detached;

                Context.Update(t);
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
