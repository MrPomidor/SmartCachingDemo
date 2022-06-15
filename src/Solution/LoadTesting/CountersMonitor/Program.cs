using System.Diagnostics;
using System.Threading;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;

namespace CountersMonitor
{
    public partial class Program
    {
        private static readonly TimeSpan CollectionTime = TimeSpan.FromMinutes(3);

        static void Main(string[] args)
        {
            try
            {
                Run();
            }
            catch(ApplicationException appEx)
            {
                Console.WriteLine(new string('-', 30));
                Console.WriteLine("Error: " + appEx.Message);
                Console.WriteLine("Press any key ...");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(new string('-', 30));
                Console.WriteLine("Unexpected exception happened: " + ex.Message);
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Press any key ...");
                Console.ReadKey();
            }
        }

        static void Run()
        {
            (var processId, var processName) = ProcessFetcher.Instance.GetDemoProcess();
            Console.WriteLine($"Found process '{processName}' with ProcessID:{processId}");
            Console.WriteLine(new string('-', 30));

            CountersStats stats;
            using (var statsCollector = new StatsCollector(processId))
            {
                stats = statsCollector.CollectStats(CollectionTime);
            }

            // TODO handle collected stats
            // TODO this is draft
            foreach (var metric in stats.CounterValues.Keys)
            {
                Console.WriteLine($"{metric}: {stats.CounterValues[metric].Value}");
            }

            Console.WriteLine("Press any key ...");
            Console.ReadKey();
        }
    }
}