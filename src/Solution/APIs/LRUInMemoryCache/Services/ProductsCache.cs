using System;
using Reusables.Caching;
using Reusables.Storage.Entities;

namespace LRUInMemoryCache.Services
{
    public class ProductsCache : IProductsCache
    {
        //private const int Capacity = 10_000;
        //private const int Capacity = 100_000;
        private const int Capacity = 20_000;
        private readonly TimeSpan TTL = TimeSpan.FromSeconds(30);

        private readonly LRUCache<long, Product> _products;
        public ProductsCache()
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
    }
}
