using System.Collections.Generic;

namespace Reusables.Monitoring
{
    public interface IProductsStatsAggregator
    {
        void ProductRequested(long productId);

        IReadOnlyDictionary<long, int> Flush();
    }
}
