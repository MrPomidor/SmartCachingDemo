using System;
using Reusables.Caching.InMemory;
using Reusables.Storage;
using Reusables.Storage.Entities;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
    [Collection("MemoryConsumptionTests")] // we need this to make tests run sequentially
    public class MemoryConsumptionTests
    {
        private const decimal BToMb = 1m / 1_000_000m;

        private readonly ITestOutputHelper _output;

        public MemoryConsumptionTests(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        /// Tests approximate amount of managed memory, consumed by LRU cache.
        /// This test always pass. Check test log in console or test runner to see memory consumption
        /// </summary>
        [Theory]
        [InlineData(1)] // ~ 24 KB
        [InlineData(10_000)] // ~ 87 MB
        [InlineData(20_000)] // ~ 173 MB
        [InlineData(50_000)] // ~ 434 MB
        [InlineData(100_000)] // ~ 876 MB
        //[InlineData(500_000)] // test with this value will consume more then 4 GB. Be carefull running it
        public void InMemoryLRUCacheConsumption(int capacity)
        {
            // use class name to initialize all static properties
            _ = typeof(LRUCache<long, Product>).Name;
            // initialize faker
            var faker = ProductsContextSeeder.GetFaker();

            GC.Collect();
            GC.WaitForFullGCComplete();
            var memoryBeforeBytes = GC.GetTotalAllocatedBytes(true);

            var items = faker.Generate(capacity);
            var lruCache = new LRUCache<long, Product>(capacity, TimeSpan.FromHours(1));
            foreach (var item in items)
            {
                lruCache.Set(item.Id, item);
            }

            items = null;
            GC.Collect();
            GC.WaitForFullGCComplete();
            var memoryAfterBytes = GC.GetTotalAllocatedBytes(true);

            Log(@$"Testing memory consumption for LRU cache with Product entity with capacity {capacity}:
                Memory before allocation: {memoryBeforeBytes * BToMb} MB ({memoryBeforeBytes} BYTES)
                Memory after allocation: {memoryAfterBytes * BToMb} MB ({memoryAfterBytes} BYTES)
                Approximate items size: {(memoryAfterBytes - memoryBeforeBytes) * BToMb} MB ({memoryAfterBytes - memoryBeforeBytes} BYTES)
            ");
        }

        private void Log(string message)
        {
            _output.WriteLine(message);
        }
    }
}
