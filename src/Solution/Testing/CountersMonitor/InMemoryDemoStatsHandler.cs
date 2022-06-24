using System;
using Contracts.Monitoring;

namespace CountersMonitor
{
    public class InMemoryDemoStatsHandler
    {
        public void HandleInMemoryDemoStats(LRUCacheCountersStats stats)
        {
            const string providerName = CountersConsts.EventSources.InMemoryLRUProductsCache;

            Console.WriteLine("Analysing data from in-memory lru demo:");

            foreach (var metricTuple in stats.GetProviderMetrics(providerName))
            {
                Console.WriteLine($"{metricTuple.metric}: {metricTuple.value}");
            }

            var notFoundItems = stats.GetMetric(providerName, CountersConsts.Metrics.InMemoryLRUProductsCache.NotFoundCount);
            var fetchedFromCacheItems = stats.GetMetric(providerName, CountersConsts.Metrics.InMemoryLRUProductsCache.FetchCount);

            var cacheEfficiencyRate = (decimal)fetchedFromCacheItems / (decimal)notFoundItems;
            Console.WriteLine($"Estimated cache efficiency rate (items fetched from cache/items failed to fetch from cache): {cacheEfficiencyRate}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
