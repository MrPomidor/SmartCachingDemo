using System.Diagnostics.Tracing;
using System.Threading;
using Contracts.Monitoring;

namespace Reusables.Monitoring
{
    [EventSource(Name = CountersConsts.EventSources.ProductsRequestStats)]
    public class ProductsStatsEventSource : EventSource
    {
        public static readonly ProductsStatsEventSource Log = new ProductsStatsEventSource();

        private int _workingSetItems = 0;

        private double _5PercentRate = 0;
        private double _10PercentRate = 0;
        private double _15PercentRate = 0;
        private double _20PercentRate = 0;
        private double _30PercentRate = 0;
        private double _50PercentRate = 0;

        private int _totalItemsCount = 0;

        private double _5PercentFromTotalRate = 0;
        private double _10PercentFromTotalRate = 0;
        private double _15PercentFromTotalRate = 0;
        private double _20PercentFromTotalRate = 0;
        private double _30PercentFromTotalRate = 0;
        private double _50PercentFromTotalRate = 0;

        private PollingCounter _workingSetCounter;

        private PollingCounter _5PercentCounter;
        private PollingCounter _10PercentCounter;
        private PollingCounter _15PercentCounter;
        private PollingCounter _20PercentCounter;
        private PollingCounter _30PercentCounter;
        private PollingCounter _50PercentCounter;

        private PollingCounter _totalItemsCountCounter;

        private PollingCounter _5PercentFromTotalCounter;
        private PollingCounter _10PercentFromTotalCounter;
        private PollingCounter _15PercentFromTotalCounter;
        private PollingCounter _20PercentFromTotalCounter;
        private PollingCounter _30PercentFromTotalCounter;
        private PollingCounter _50PercentFromTotalCounter;

        public ProductsStatsEventSource()
        {

        }

        public void SetStats(
            double _5percentRate,
            double _10percentRate,
            double _15percentRate,
            double _20percentRate,
            double _30percentRate,
            double _50percentRate,
            int workingSetITems
            )
        {
            Interlocked.Exchange(ref _5PercentRate, _5percentRate);
            Interlocked.Exchange(ref _10PercentRate, _10percentRate);
            Interlocked.Exchange(ref _15PercentRate, _15percentRate);
            Interlocked.Exchange(ref _20PercentRate, _20percentRate);
            Interlocked.Exchange(ref _30PercentRate, _30percentRate);
            Interlocked.Exchange(ref _50PercentRate, _50percentRate);
            Interlocked.Exchange(ref _workingSetItems, workingSetITems);
        }

        public void SetStatsFromTotal(
            double _5percentRate,
            double _10percentRate,
            double _15percentRate,
            double _20percentRate,
            double _30percentRate,
            double _50percentRate,
            int totalItemsCount
            )
        {
            Interlocked.Exchange(ref _5PercentFromTotalRate, _5percentRate);
            Interlocked.Exchange(ref _10PercentFromTotalRate, _10percentRate);
            Interlocked.Exchange(ref _15PercentFromTotalRate, _15percentRate);
            Interlocked.Exchange(ref _20PercentFromTotalRate, _20percentRate);
            Interlocked.Exchange(ref _30PercentFromTotalRate, _30percentRate);
            Interlocked.Exchange(ref _50PercentFromTotalRate, _50percentRate);
            Interlocked.Exchange(ref _totalItemsCount, totalItemsCount);
        }

        protected override void OnEventCommand(EventCommandEventArgs command)
        {
            if (command.Command == EventCommand.Enable)
            {
                _workingSetCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.WorkingSetItemsCount, this, () => _workingSetItems);

                _5PercentCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_5Percent, this, () => _5PercentRate);
                _10PercentCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_10Percent, this, () => _10PercentRate);
                _15PercentCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_15Percent, this, () => _15PercentRate);
                _20PercentCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_20Percent, this, () => _20PercentRate);
                _30PercentCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_30Percent, this, () => _30PercentRate);
                _50PercentCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_50Percent, this, () => _50PercentRate);

                _totalItemsCountCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.TotalItemsCount, this, () => _totalItemsCount);

                _5PercentFromTotalCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_5Percent, this, () => _5PercentFromTotalRate);
                _10PercentFromTotalCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_10Percent, this, () => _10PercentFromTotalRate);
                _15PercentFromTotalCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_15Percent, this, () => _15PercentFromTotalRate);
                _20PercentFromTotalCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_20Percent, this, () => _20PercentFromTotalRate);
                _30PercentFromTotalCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_30Percent, this, () => _30PercentFromTotalRate);
                _50PercentFromTotalCounter ??= new PollingCounter(CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_50Percent, this, () => _50PercentFromTotalRate);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _workingSetCounter?.Dispose();
            _workingSetCounter = null;

            _5PercentCounter?.Dispose();
            _5PercentCounter = null;

            _10PercentCounter?.Dispose();
            _10PercentCounter = null;

            _15PercentCounter?.Dispose();
            _15PercentCounter = null;

            _20PercentCounter?.Dispose();
            _20PercentCounter = null;

            _30PercentCounter?.Dispose();
            _30PercentCounter = null;

            _50PercentCounter?.Dispose();
            _50PercentCounter = null;


            _totalItemsCountCounter?.Dispose();
            _totalItemsCountCounter = null;

            _5PercentFromTotalCounter?.Dispose();
            _5PercentFromTotalCounter = null;

            _10PercentFromTotalCounter?.Dispose();
            _10PercentFromTotalCounter = null;

            _15PercentFromTotalCounter?.Dispose();
            _15PercentFromTotalCounter = null;

            _20PercentFromTotalCounter?.Dispose();
            _20PercentFromTotalCounter = null;

            _30PercentFromTotalCounter?.Dispose();
            _30PercentFromTotalCounter = null;

            _50PercentFromTotalCounter?.Dispose();
            _50PercentFromTotalCounter = null;

            base.Dispose(disposing);
        }

    }
}
