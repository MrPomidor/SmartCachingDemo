using System.Collections.Concurrent;

namespace CountersMonitor
{
    public class CountersStats
    {
        public ConcurrentDictionary<string, CountersStatsItem> CounterValues { get; private set; } = new ConcurrentDictionary<string, CountersStatsItem>();
        public void AddMetric(string key, long value)
        {
            CounterValues.AddOrUpdate(key, new CountersStatsItem { Value = value }, (key, old) => old.Add(value));
        }
        // TODO probably add GET method too
    }

    public class CountersStatsItem
    {
        public long Value;
        public CountersStatsItem Add(long value)
        {
            Interlocked.Add(ref Value, value);
            return this;
        }
    }
}
