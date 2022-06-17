using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contracts.Monitoring;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;

namespace CountersMonitor
{
    /// <summary>
    /// Code which collect stats was copied from/inspired by `dotnet-counters` tool source code. 
    /// Stats collector repeats some code, which is used by 'monitor' command.
    /// Source code could be found in this repository: https://github.com/dotnet/diagnostics/tree/main/src/Tools/dotnet-counters
    /// More on reading data from counters out-of-proc you can read in next articles: 
    /// https://github.com/dotnet/diagnostics/blob/main/documentation/design-docs/eventcounters.md
    /// https://github.com/dotnet/diagnostics/blob/c13a550fd8135e031678015743557ea2a543403f/documentation/design-docs/diagnostics-client-library.md
    /// </summary>
    public class StatsCollector : IDisposable
    {
        private readonly int _processId;

        private EventPipeSession _session;
        private CancellationTokenSource _cancellationTokenSource;

        public StatsCollector(int processId)
        {
            _processId = processId;
        }

        public CountersStats CollectStats(TimeSpan collectionTime)
        {
            Console.WriteLine($"Start collecting stats for ProcessID:{_processId}");
            Console.WriteLine($"Stats will be collected during next {collectionTime.Minutes} minutes, {collectionTime.Seconds} seconds");
            Console.WriteLine("To stop collection earlier - press 'Q'");

            var stats = new CountersStats();

            _cancellationTokenSource = new CancellationTokenSource();

            var listenerTask = CreateListenerTask(_processId, stats, _cancellationTokenSource);
            listenerTask.Start();

            var durationStopwatch = Stopwatch.StartNew();
            bool statsCollectionFailed = false;
            while (true)
            {
                // if cancellation requested from listener, this means exception while processing event source
                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    statsCollectionFailed = true;
                    break;
                }

                if (!Console.IsInputRedirected && Console.KeyAvailable)
                {
                    ConsoleKey cmd = Console.ReadKey(true).Key;
                    if (cmd == ConsoleKey.Q)
                    {
                        break;
                    }
                }

                if (durationStopwatch.Elapsed >= collectionTime)
                {
                    break;
                }

                Thread.Sleep(100);
            }

            durationStopwatch.Stop();
            try
            {
                _cancellationTokenSource?.Cancel();
            }
            catch { }

            if (statsCollectionFailed)
                throw new ApplicationException("Collecting stats failed");

            Console.WriteLine(new string('-', 30));
            Console.WriteLine("Stats collection completed");
            Console.WriteLine($"Time: {durationStopwatch.Elapsed.Minutes} minutes, {durationStopwatch.Elapsed.Seconds} seconds");
            Console.WriteLine(new string('-', 30));

            return stats;
        }

        private static readonly string[] DemoProviderNames = new[]
        {
            CountersConsts.EventSources.InMemoryLRUProductsCache,
            CountersConsts.EventSources.RedisLRUProductsCache
        };

        private Task CreateListenerTask(int processId, CountersStats stats, CancellationTokenSource cancellationTokenSource)
        {
            return new Task(() =>
            {
                var sessionId = Guid.NewGuid().ToString();
                var providers = GetEventPipeProviders(DemoProviderNames, sessionId);

                var client = new DiagnosticsClient(processId);

                _session = client.StartEventPipeSession(providers, false, 10);
                using (var source = new EventPipeEventSource(_session.EventStream))
                {
                    var cancellationToken = cancellationTokenSource.Token;

                    cancellationToken.Register(() => source.StopProcessing());

                    try
                    {
                        client.ResumeRuntime();
                    }
                    catch { }

                    source.Dynamic.All += GetTraceEventHandler(stats, cancellationToken);

                    try
                    {
                        source.Process();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error encountered while processing events");
                        Console.WriteLine(e.ToString());

                        try
                        {
                            cancellationTokenSource.Cancel();
                        }
                        catch { }
                    }
                }
            });
        }

        private Action<TraceEvent> GetTraceEventHandler(CountersStats stats, CancellationToken cancellationToken)
        {
            return (TraceEvent obj) =>
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                if (obj.EventName == "EventCounters")
                {
                    HandleDiagnosticCounter(obj, stats);
                }
            };
        }

        private void HandleDiagnosticCounter(TraceEvent obj, CountersStats stats)
        {
            IDictionary<string, object> payloadVal = (IDictionary<string, object>)(obj.PayloadValue(0));
            IDictionary<string, object> payloadFields = (IDictionary<string, object>)(payloadVal["Payload"]);

            // If it's not a counter we asked for, ignore it.
            if (!DemoProviderNames.Contains(obj.ProviderName)) return;

            if (payloadFields["CounterType"].Equals("Sum"))
            {
                var name = payloadFields["Name"].ToString();
                var increment = (double)payloadFields["Increment"];

                stats.AddMetric(obj.ProviderName, name, (long)increment);
            }
        }

        /// <summary>
        /// https://github.com/dotnet/diagnostics/blob/0bcc6110ea09bdd83d6ad637a175b0f86f0191d0/src/Tools/dotnet-counters/CounterMonitor.cs#L770
        /// </summary>
        private EventPipeProvider[] GetEventPipeProviders(IEnumerable<string> providers, string sessionId)
        {
            var interval = 1;

            // EventSources support EventCounter based metrics directly
            IEnumerable<EventPipeProvider> eventCounterProviders = providers.Select(
                providerName => new EventPipeProvider(providerName, EventLevel.Error, 0, new Dictionary<string, string>()
                {{ "EventCounterIntervalSec", interval.ToString() }}));

            //System.Diagnostics.Metrics EventSource supports the new Meter/Instrument APIs
            const long TimeSeriesValues = 0x2;
            StringBuilder metrics = new StringBuilder();
            foreach (string provider in providers)
            {
                if (metrics.Length != 0)
                {
                    metrics.Append(',');
                }
                metrics.Append(provider);
            }
            EventPipeProvider metricsEventSourceProvider =
                new EventPipeProvider("System.Diagnostics.Metrics", EventLevel.Informational, TimeSeriesValues,
                    new Dictionary<string, string>()
                    {
                        { "SessionId", sessionId },
                        { "Metrics", metrics.ToString() },
                        { "RefreshInterval", interval.ToString() },
                        { "MaxTimeSeries", 1000.ToString() },
                        { "MaxHistograms", 10.ToString() }
                    }
                );

            return eventCounterProviders.Append(metricsEventSourceProvider).ToArray();
        }

        public void Dispose()
        {
            try
            {
                _session?.Dispose();
            }
            catch { }
        }
    }
}