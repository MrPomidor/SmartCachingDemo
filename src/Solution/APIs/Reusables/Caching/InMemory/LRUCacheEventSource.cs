using System.Diagnostics.Tracing;
using Contracts.Monitoring;

namespace Reusables.Caching.InMemory
{
    [EventSource(Name = CountersConsts.EventSources.InMemoryLRUProductsCache)]
    public class LRUCacheEventSource : EventSource
    {
        public static readonly LRUCacheEventSource Log = new LRUCacheEventSource();

        private IncrementingEventCounter _addedCounter;
        private IncrementingEventCounter _fetchCounter;
        private IncrementingEventCounter _expiredCounter;
        private IncrementingEventCounter _notFoundCounter;
        private IncrementingEventCounter _requestedCounter;

        public LRUCacheEventSource()
        {

        }

        public void ItemRequested()
        {
            _requestedCounter?.Increment();
        }

        public void ItemAdded()
        {
            _addedCounter?.Increment();
        }

        public void ItemFetched()
        {
            _fetchCounter?.Increment();
        }

        public void ItemExpired()
        {
            _expiredCounter?.Increment();
        }

        public void ItemNotFound()
        {
            _notFoundCounter?.Increment();
        }

        protected override void OnEventCommand(EventCommandEventArgs command)
        {
            if (command.Command == EventCommand.Enable)
            {
                _addedCounter ??= new IncrementingEventCounter(CountersConsts.Metrics.InMemoryLRUProductsCache.AddCount, this);
                _fetchCounter ??= new IncrementingEventCounter(CountersConsts.Metrics.InMemoryLRUProductsCache.FetchCount, this);
                _expiredCounter ??= new IncrementingEventCounter(CountersConsts.Metrics.InMemoryLRUProductsCache.ExpireCount, this);
                _notFoundCounter ??= new IncrementingEventCounter(CountersConsts.Metrics.InMemoryLRUProductsCache.NotFoundCount, this);
                _requestedCounter ??= new IncrementingEventCounter(CountersConsts.Metrics.InMemoryLRUProductsCache.RequestedCount, this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _addedCounter?.Dispose();
            _addedCounter = null;

            _fetchCounter?.Dispose();
            _fetchCounter = null;

            _expiredCounter?.Dispose();
            _expiredCounter = null;

            _notFoundCounter?.Dispose();
            _notFoundCounter = null;

            _requestedCounter?.Dispose();
            _requestedCounter = null;

            base.Dispose(disposing);
        }
    }
}
