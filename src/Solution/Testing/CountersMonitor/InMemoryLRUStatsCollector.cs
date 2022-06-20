using System;
using System.Collections.Generic;
using Contracts.Monitoring;
using Microsoft.Diagnostics.Tracing;

namespace CountersMonitor
{
    public class InMemoryLRUStatsCollector : StatsCollectorBase<LRUCacheCountersStats>
    {
        public InMemoryLRUStatsCollector(int processId) : base(processId)
        {
        }

        private static readonly string ProviderName = CountersConsts.EventSources.InMemoryLRUProductsCache;

        protected override string[] GetProviderNames() => new[] { ProviderName };

        protected override void HandleDiagnosticCounter(TraceEvent obj, LRUCacheCountersStats stats)
        {
            IDictionary<string, object> payloadVal = (IDictionary<string, object>)(obj.PayloadValue(0));
            IDictionary<string, object> payloadFields = (IDictionary<string, object>)(payloadVal["Payload"]);

            // If it's not a counter we asked for, ignore it.
            if (!string.Equals(obj.ProviderName, ProviderName, StringComparison.InvariantCultureIgnoreCase)) return;

            if (payloadFields["CounterType"].Equals("Sum"))
            {
                var name = payloadFields["Name"].ToString();
                var increment = (double)payloadFields["Increment"];

                stats.AddMetric(obj.ProviderName, name, (long)increment);
            }
        }
    }
}
