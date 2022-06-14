using System.Diagnostics.Tracing;
using Contracts.Monitoring;

namespace Reusables.Caching.Redis
{
    [EventSource(Name = CountersConsts.EventSources.RedisLRUProductsCache)]
    public class ProductsRedisCacheEventSource : EventSource
    {
        public static readonly ProductsRedisCacheEventSource Log = new ProductsRedisCacheEventSource();

        private IncrementingEventCounter _addedCounter;
        private IncrementingEventCounter _fetchCounter;
        private IncrementingEventCounter _notFoundCounter;

        public ProductsRedisCacheEventSource()
        {

        }

        public void ItemAdded()
        {
            _addedCounter.Increment();
        }

        public void ItemFetched()
        {
            _fetchCounter.Increment();
        }

        public void ItemNotFound()
        {
            _notFoundCounter.Increment();
        }

        protected override void OnEventCommand(EventCommandEventArgs command)
        {
            if (command.Command == EventCommand.Enable)
            {
                _addedCounter ??= new IncrementingEventCounter(CountersConsts.Metrics.RedisLRUProductsCache.AddCount, this);
                _fetchCounter ??= new IncrementingEventCounter(CountersConsts.Metrics.RedisLRUProductsCache.FetchCount, this);
                _notFoundCounter ??= new IncrementingEventCounter(CountersConsts.Metrics.RedisLRUProductsCache.NotFoundCount, this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _addedCounter?.Dispose();
            _addedCounter = null;

            _fetchCounter?.Dispose();
            _fetchCounter = null;

            _notFoundCounter?.Dispose();
            _notFoundCounter = null;

            base.Dispose(disposing);
        }
    }
}
