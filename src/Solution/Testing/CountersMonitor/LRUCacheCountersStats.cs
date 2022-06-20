using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CountersMonitor
{
    public class LRUCacheCountersStats
    {
        private ConcurrentDictionary<string, LRUCacheCountersStatsItem> _counterValues { get; set; } = new ConcurrentDictionary<string, LRUCacheCountersStatsItem>();

        public void AddMetric(string providerName, string metric, long value)
        {
            _counterValues.AddOrUpdate(GetKey(providerName, metric), new LRUCacheCountersStatsItem { Value = value }, (key, old) => old.Add(value));
        }

        public long GetMetric(string providerName, string metric)
        {
            if (_counterValues.TryGetValue(GetKey(providerName, metric), out var stats))
                return stats.Value;
            return 0;
        }

        public IEnumerable<(string metric, long value)> GetProviderMetrics(string providerName) => _counterValues
            .Where(x => x.Key.StartsWith(providerName))
            .Select(x => (GetMetric(x.Key), x.Value.Value));

        private string GetKey(string providerName, string metric) => $"{providerName}/{metric}";
        private string GetMetric(string key) => key.Split('/', StringSplitOptions.RemoveEmptyEntries)[1];
    }

    public class LRUCacheCountersStatsItem
    {
        public long Value;
        public LRUCacheCountersStatsItem Add(long value)
        {
            Interlocked.Add(ref Value, value);
            return this;
        }
    }
}
