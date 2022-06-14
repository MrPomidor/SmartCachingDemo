using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Reusables.Caching.Redis;
using Reusables.Storage;
using Reusables.Utils;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    [Collection("MemoryConsumptionTests")] // we need this to make tests run sequentially
    public class RedisMemoryConsumptionTest : IDisposable
    {
        private const decimal BToMb = 1m / 1_000_000m;

        private readonly ITestOutputHelper _output;
        private readonly IRedisManager _redisManager;

        public RedisMemoryConsumptionTest(ITestOutputHelper output)
        {
            _output = output;
            _redisManager = GetRedisManager();
        }

        public void Dispose()
        {
            _redisManager?.Dispose();
        }

        /// <summary>
        /// Tests how much memory items consume in Redis. 
        /// This test always pass. Check test log in console or test runner to see memory consumption. 
        /// </summary>
        [Theory]
        [InlineData(1)] // ~ 0.00063 MB
        [InlineData(50)] // ~ 0.0336 MB
        [InlineData(100)] // ~ 0.0677 MB
        [InlineData(1_000)] // ~ 0.641648 MB
        [InlineData(10_000)] // ~ 6.918 MB
        [InlineData(50_000)] // ~ 34.31 MB
        [InlineData(100_000)] // ~ 68.63 MB
        public async Task RedisMemoryConsumption(int elementsCount)
        {
            // arrange
            await _redisManager.ClearCache();
            var productsCache = new ProductsRedisCache(new SystemTextJsonSerializer(), _redisManager);

            var faker = ProductsContextSeeder.GetFaker();
            var items = faker.Generate(elementsCount);

            var usedMemoryBeforeBytes = await _redisManager.GetUsedMemoryBytes();

            // act
            await Parallel.ForEachAsync(items, async (item, _) =>
            {
                await productsCache.Set(item.Id, item);
            });

            // assert
            var usedMemoryAfterBytes = await _redisManager.GetUsedMemoryBytes();
            LogTest($@"Testing memory consumption in Redis for Product entity with items count: {elementsCount}
                Used memory before: {usedMemoryBeforeBytes * BToMb} MB ({usedMemoryBeforeBytes} BYTES)
                Used memory after: {usedMemoryAfterBytes * BToMb} MB ({usedMemoryAfterBytes} BYTES)
                Approximate memory taken in Redis: {(usedMemoryAfterBytes - usedMemoryBeforeBytes) * BToMb} MB ({usedMemoryAfterBytes - usedMemoryBeforeBytes} BYTES)
            ");

            // cleanup
            await _redisManager.ClearCache();
        }

        /// <summary>
        /// Test purpose - ability to control maxmemory limit in Redis on the fly.
        /// This is needed to control when redis LRU eviction algorithm will start working.
        /// </summary>
        [Fact(Skip = "Manual running only")]
        public async Task SetRedisMaxMemory()
        {
            const int maxMemoryMb = 100;

            await _redisManager.SetMaxMemory(maxMemoryMb);

            LogTest($"Redis maxmemory was set to {maxMemoryMb} MB");
        }

        private IRedisManager GetRedisManager()
        {
            var condifurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false);

            var config = condifurationBuilder.Build();

            var redisConnectionString = config.GetConnectionString("Redis");

            var redisManager = new RedisManager(redisConnectionString);
            return redisManager;
        }

        private void LogTest(string message)
        {
            _output.WriteLine(message);
        }
    }
}
