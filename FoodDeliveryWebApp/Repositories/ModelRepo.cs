using FoodDeliveryWebApp.Contracts;
using FoodDeliveryWebApp.Data;
using FoodDeliveryWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;

namespace FoodDeliveryWebApp.Repositories
{
    public class ModelRepo<T> : IModelRepo<T> where T : BaseModel
    {
        public ModelRepo(FoodDeliveryWebAppContext context)
        {
            Context = context;
            Query = Context.Set<T>();
        }
        protected FoodDeliveryWebAppContext Context { get; }
        public IQueryable<T> Query { get; set; }

        public virtual List<T> GetAll()
        {
            return Query.ToList();
        }

        public virtual T? GetById<U>(U? id)
        {
            return Query.FirstOrDefault(t => t.Id.Equals(id));
        }

        public virtual bool TryDelete(dynamic? id)
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
            return Query.Where(lambda).ToList();
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
        public virtual bool TryInsert(Product t, IFormFile? Image) { return false; }
        public virtual bool TryUpdate(Product t, IFormFile? Image) { return false; }

        public virtual bool TryUpdate(T t)
        {
            try
            {
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
