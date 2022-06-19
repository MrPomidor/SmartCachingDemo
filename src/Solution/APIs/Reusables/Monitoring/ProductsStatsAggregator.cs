using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Reusables.Monitoring
{
    public class ProductsStatsAggregator : IProductsStatsAggregator
    {
        private const int ExpectedCapacity = 1_000_000; // TODO config ?

        private ConcurrentDictionary<long, int> _stats;

        public ProductsStatsAggregator()
        {
            _stats = CreateNewStats();
        }

        public IReadOnlyDictionary<long, int> Flush()
        {
            var newStats = CreateNewStats();
            var currStats = Interlocked.Exchange(ref _stats, newStats);

            return currStats;
        }

        public void ProductRequested(long productId)
        {
            _stats.AddOrUpdate(productId, 0, (key, oldValue) => oldValue++);
        }

        private ConcurrentDictionary<long, int> CreateNewStats()
        {
            int numProcs = Environment.ProcessorCount;
            int concurrencyLevel = numProcs * 2;

            var newStats = new ConcurrentDictionary<long, int>(concurrencyLevel, ExpectedCapacity);
            return newStats;
        }
    }
}
