using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reusables.Caching;
using Reusables.Caching.InMemory;
using Reusables.Caching.Redis;
using Reusables.Utils;

namespace Reusables.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryCache(this IServiceCollection services)
        {
            services.AddSingleton<IProductsCache, ProductsInMemoryCache>();

            return services;
        }

        public static IServiceCollection AddRedisCache(this IServiceCollection services)
        {
            services.AddSingleton<ISerializer, SystemTextJsonSerializer>();
            services.AddSingleton<IRedisManager>((IServiceProvider provider) =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                var redisConnectionString = config.GetConnectionString("Redis");
                if (string.IsNullOrEmpty(redisConnectionString))
                    throw new ApplicationException("Cannot read Redis connection string");

                return new RedisManager(redisConnectionString);
            });
            services.AddSingleton<IProductsCache, ProductsRedisCache>();

            return services;
        }
    }
}
