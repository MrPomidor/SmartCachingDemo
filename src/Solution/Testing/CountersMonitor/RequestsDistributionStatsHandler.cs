using System;

namespace CountersMonitor
{
    public class RequestsDistributionStatsHandler
    {
        public void HandleRequestsDistributionStats(RequestsDistributionStats stats)
        {
            Console.WriteLine("Analysing requests distribution from no cache demo:");

            Console.WriteLine(@$"Working set items
                 Working set items count: {stats.WorkingSetItemsCount}
                5  Percent Request quota: {stats._5percentRequestPart}
                10 Percent Request quota: {stats._10percentRequestPart}
                15 Percent Request quota: {stats._15percentRequestPart}
                20 Percent Request quota: {stats._20percentRequestPart}
                30 Percent Request quota: {stats._30percentRequestPart}
                50 Percent Request quota: {stats._50percentRequestPart}
            ");

            Console.WriteLine(@$"All items
                                    All items count: {stats.TotalItemsCount}
                5  Percent From Total Request quota: {stats._5percentRequestFromTotalPart}
                10 Percent From Total Request quota: {stats._10percentRequestFromTotalPart}
                15 Percent From Total Request quota: {stats._15percentRequestFromTotalPart}
                20 Percent From Total Request quota: {stats._20percentRequestFromTotalPart}
                30 Percent From Total Request quota: {stats._30percentRequestFromTotalPart}
                50 Percent From Total Request quota: {stats._50percentRequestFromTotalPart}
            ");
        }
    }
}
