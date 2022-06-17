using System;
using Contracts.Monitoring;

namespace CountersMonitor
{
    public class StatsHandler
    {
        public static readonly StatsHandler Instance = new StatsHandler();

        private StatsHandler() { }

        public void Handle(CountersStats stats, DemoTypes demoType)
        {
            switch (demoType)
            {
                case DemoTypes.InMemoryLRU:
                    HandleInMemoryDemoStats(stats);
                    break;
                case DemoTypes.RedisLRU:
                    HandleRedisDemoStats(stats);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Demo type {demoType} is not supported");
            }
        }

        private void HandleInMemoryDemoStats(CountersStats stats)
        {
            const string providerName = CountersConsts.EventSources.InMemoryLRUProductsCache;

            Console.WriteLine("Analysing data from in-memory lru demo:");

            foreach (var metricTuple in stats.GetProviderMetrics(providerName))
            {
                Console.WriteLine($"{metricTuple.metric}: {metricTuple.value}");
            }

            var totalRequestedItems = stats.GetMetric(providerName, CountersConsts.Metrics.InMemoryLRUProductsCache.RequestedCount);
            var fetchedFromCacheItems = stats.GetMetric(providerName, CountersConsts.Metrics.InMemoryLRUProductsCache.FetchCount);

            var cacheEfficiencyRate = (decimal)fetchedFromCacheItems / (decimal)totalRequestedItems;
            Console.WriteLine($"Estimated cache efficiency rate (items fetched from cache/total items requested): {cacheEfficiencyRate}");
            Console.WriteLine(new string('-', 30));
        }

        private void HandleRedisDemoStats(CountersStats stats)
        {
            const string providerName = CountersConsts.EventSources.RedisLRUProductsCache;

            Console.WriteLine("Analysing data from redis lru demo:");

            foreach (var metricTuple in stats.GetProviderMetrics(providerName))
            {
                Console.WriteLine($"{metricTuple.metric}: {metricTuple.value}");
            }

            var totalRequestedItems = stats.GetMetric(providerName, CountersConsts.Metrics.RedisLRUProductsCache.RequestedCount);
            var fetchedFromCacheItems = stats.GetMetric(providerName, CountersConsts.Metrics.RedisLRUProductsCache.FetchCount);

            var cacheEfficiencyRate = (decimal)fetchedFromCacheItems / (decimal)totalRequestedItems;
            Console.WriteLine($"Estimated cache efficiency rate (items fetched from cache/total items requested): {cacheEfficiencyRate}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
