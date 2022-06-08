using Reusables.Storage.Entities;

namespace LRUInMemoryCache.Services
{
    public interface IProductsCache
{
        bool TryGet(long key, out Product value);
        void Set(long key, Product value);
    }
}
