using System;
using Contracts.Monitoring;

namespace CountersMonitor
{
    public class RedisDemoStatsHandler
    {
        public void HandleRedisDemoStats(LRUCacheCountersStats stats)
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
