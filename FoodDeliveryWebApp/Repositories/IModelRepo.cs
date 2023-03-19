using FoodDeliveryWebApp.Models;

namespace FoodDeliveryWebApp.Repositories
{
    public interface IModelRepo<T> where T : BaseModel
    {
        public List<T> GetAll();
        public T? GetById(int? id);
        public bool TryInsert(T t);
        public bool TryUpdate(T t);
        public bool TryDelete(int? id);
        public List<T> Where(Func<T, bool> lambda);
    }
}
