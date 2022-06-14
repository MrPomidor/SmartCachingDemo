using System;
using System.Threading.Tasks;
using Reusables.Storage.Entities;
using Reusables.Utils;
using StackExchange.Redis;

namespace Reusables.Caching.Redis
{
    public class ProductsRedisCache : IProductsCache
    {
        private static readonly TimeSpan TTL = TimeSpan.FromSeconds(30);

        private readonly ISerializer _serializer;
        private readonly IRedisManager _redisManager;

        public ProductsRedisCache(
            ISerializer serializer,
            IRedisManager redisManager
            )
        {
            _serializer = serializer;
            _redisManager = redisManager;
        }

        public async ValueTask Set(long productId, Product value)
        {
            var key = GetKey(productId);
            var valueSerialized = _serializer.Serialize(value);

            await _redisManager.SetValue(key, valueSerialized, TTL);
        }

        public async ValueTask<Product> TryGet(long productId)
        {
            var redisValue = await _redisManager.GetValue(GetKey(productId));
            if (!redisValue.HasValue)
                return null;

            var valueDeserialized = _serializer.Deserialize<Product>(redisValue);
            return valueDeserialized;
        }

        private static RedisKey GetKey(long productId) => $"Product-{productId}";
    }
}
