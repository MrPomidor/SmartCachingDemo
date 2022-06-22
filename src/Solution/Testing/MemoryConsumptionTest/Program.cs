using System;
using Reusables.Caching.InMemory;
using Reusables.Storage;
using Reusables.Storage.Entities;

namespace MemoryConsumptionTest
{
    /// <summary>
    /// Tests approximate amount of managed memory, consumed by LRU cache.
    /// </summary>
    internal class Program
    {
        private const decimal BToMb = 1m / 1_000_000m;

        static void Main(string[] args)
        {
            const int capacity = 1; // ~ 1.272 KB
            //const int capacity = 10_000; // ~ 9.24 MB
            //const int capacity = 20_000; // ~ 18.47 MB
            //const int capacity = 50_000; // ~ 46.16 MB
            //const int capacity = 100_000; // ~ 92.45 MB
            //const int capacity = 500_000; // ~ 463 MB
            //const int capacity = 1_000_000; // ~ 928 MB

            var faker = ProductsContextSeeder.GetFaker();
            var items = faker.Generate(capacity);
            var lruCache = new LRUCache<long, Product>(capacity, TimeSpan.FromHours(1));
            foreach (var item in items)
            {
                lruCache.Set(item.Id, item);
            }

            faker = null;
            items = null;

            var memoryBeforeBytes = GC.GetTotalMemory(true);

            lruCache.ToString();
            lruCache = null;

            var memoryAfterBytes = GC.GetTotalMemory(true);

            Log(@$"Testing memory consumption for LRU cache with Product entity with capacity {capacity}:
                Memory before cleanup: {memoryBeforeBytes * BToMb} MB ({memoryBeforeBytes} BYTES)
                Memory after cleanup: {memoryAfterBytes * BToMb} MB ({memoryAfterBytes} BYTES)
                Approximate items size: {(memoryBeforeBytes - memoryAfterBytes) * BToMb} MB ({memoryBeforeBytes - memoryAfterBytes} BYTES)
            ");

            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }

        static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}