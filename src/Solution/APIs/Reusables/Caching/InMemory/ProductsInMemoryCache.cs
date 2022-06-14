using System;
using System.Threading.Tasks;
using Reusables.Storage.Entities;

namespace Reusables.Caching.InMemory
{
    public class ProductsInMemoryCache : IProductsCache
    {
        //private const int Capacity = 10_000;
        //private const int Capacity = 100_000;
        private const int Capacity = 20_000;
        private readonly TimeSpan TTL = TimeSpan.FromSeconds(30);

        private readonly LRUCache<long, Product> _products;
        public ProductsInMemoryCache()
        {
            _products = new LRUCache<long, Product>(Capacity, TTL);
        }

        public void Set(long key, Product value)
        {
            _products.Set(key, value);
        }

        public bool TryGet(long key, out Product value)
        {
            return _products.TryGet(key, out value);
        }

        private static readonly ValueTask<Product> NullProductValueTask = ValueTask.FromResult((Product)null);
        public ValueTask<Product> TryGet(long key)
        {
            if (_products.TryGet(key, out var value))
                return ValueTask.FromResult(value);
            return NullProductValueTask;
        }

        ValueTask IProductsCache.Set(long key, Product value)
        {
            _products.Set(key, value);
            return ValueTask.CompletedTask;
        }
    }
}
