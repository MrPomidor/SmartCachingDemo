using System;
using System.Threading.Tasks;
using Reusables.Storage.Entities;

namespace Reusables.Caching.InMemory
{
    public class ProductsInMemoryCache : IProductsCache
    {
        private const int Capacity = 5_000;
        //private const int Capacity = 13_000;
        //private const int Capacity = 15_000;
        private readonly TimeSpan TTL = TimeSpan.FromSeconds(30);

        private readonly LRUCache<long, Product> _products;
        public ProductsInMemoryCache()
        {
            _products = new LRUCache<long, Product>(Capacity, TTL);
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
