using FoodDeliveryWebApp.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace FoodDeliveryWebApp.Contracts
{
    public interface IModelRepo<T> where T : BaseModel
    {
        public List<T> GetAll();
        public T? GetById<U>(U? id);
        public bool TryInsert(T t);
        public bool TryUpdate(T t);
        public bool TryDelete(dynamic? id);
        public List<T> Where(Func<T, bool> lambda);

        public IQueryable<T> Query { get; set; }
    }
}
