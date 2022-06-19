using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reusables.Caching;
using Reusables.Caching.InMemory;
using Reusables.Caching.Redis;
using Reusables.Monitoring;
using Reusables.Storage;
using Reusables.Utils;

namespace Reusables.DI
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDb(this IServiceCollection services, ConfigurationManager config)
        {
            services.AddDbContext<ProductsContext>((optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(config.GetConnectionString("ProductsDB"));
            });
            return services;
        }

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

        public static IServiceCollection AddProductStatsCollection(this IServiceCollection services)
        {
            services.AddSingleton<IProductsStatsAggregator, ProductsStatsAggregator>();
            services.AddHostedService<ProductsStatsProcessor>();
            return services;
        }
    }
}
