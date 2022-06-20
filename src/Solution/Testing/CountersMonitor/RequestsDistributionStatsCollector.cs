using System;
using System.Collections.Generic;
using Contracts.Monitoring;
using Microsoft.Diagnostics.Tracing;

namespace CountersMonitor
{
    public class RequestsDistributionStatsCollector : StatsCollectorBase<RequestsDistributionStats>
    {
        public RequestsDistributionStatsCollector(int processId) : base(processId)
        {
        }

        private static readonly string ProviderName = CountersConsts.EventSources.ProductsRequestStats;

        protected override string[] GetProviderNames() => new[] { ProviderName };

        private static Dictionary<string, Action<RequestsDistributionStats, double>> _counterSetValueMap = new Dictionary<string, Action<RequestsDistributionStats, double>>
        {
            { CountersConsts.Metrics.ProductsRequestStats.WorkingSetItemsCount, (stats, val) => stats.WorkingSetItemsCount = val },
            { CountersConsts.Metrics.ProductsRequestStats.TotalItemsCount, (stats, val) => stats.TotalItemsCount = val },

            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_5Percent, (stats, val) => stats._5percentRequestPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_10Percent, (stats, val) => stats._10percentRequestPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_15Percent, (stats, val) => stats._15percentRequestPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_20Percent, (stats, val) => stats._20percentRequestPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_30Percent, (stats, val) => stats._30percentRequestPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_50Percent, (stats, val) => stats._50percentRequestPart = val },

            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_5Percent, (stats, val) => stats._5percentRequestFromTotalPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_10Percent, (stats, val) => stats._10percentRequestFromTotalPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_15Percent, (stats, val) => stats._15percentRequestFromTotalPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_20Percent, (stats, val) => stats._20percentRequestFromTotalPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_30Percent, (stats, val) => stats._30percentRequestFromTotalPart = val },
            { CountersConsts.Metrics.ProductsRequestStats.RequestQuota_FromTotal_50Percent, (stats, val) => stats._50percentRequestFromTotalPart = val },
        };

        protected override void HandleDiagnosticCounter(TraceEvent obj, RequestsDistributionStats stats)
        {
            IDictionary<string, object> payloadVal = (IDictionary<string, object>)(obj.PayloadValue(0));
            IDictionary<string, object> payloadFields = (IDictionary<string, object>)(payloadVal["Payload"]);

            // If it's not a counter we asked for, ignore it.
            if (!string.Equals(obj.ProviderName, ProviderName, StringComparison.InvariantCultureIgnoreCase)) return;

            if (payloadFields["CounterType"].Equals("Mean"))
            {
                var name = payloadFields["Name"].ToString();
                if (!_counterSetValueMap.ContainsKey(name))
                    return;

                var mean = (double)payloadFields["Mean"];
                _counterSetValueMap[name](stats, mean);
            }

            //Console.WriteLine(obj);
            //throw new NotImplementedException();
        }
    }
}
