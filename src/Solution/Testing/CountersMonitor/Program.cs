using System;

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

                Console.WriteLine("Press any key ...");
                Console.ReadKey();
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
            (var processId, var demoType) = new ProcessFetcher().GetDemoProcess();

            switch (demoType)
            {
                case DemoTypes.InMemoryLRU:
                    HandleInMemoryLRU(processId);
                    break;
                case DemoTypes.RedisLRU:
                    HandleRedisLRU(processId);
                    break;
                case DemoTypes.NoCacheCollectDistribution:
                    HandleNoCacheCollectDistribution(processId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Demo type {demoType} is not supported");
            }
        }

        static void HandleInMemoryLRU(int processId)
        {
            LRUCacheCountersStats stats;
            using (var statsCollector = new InMemoryLRUStatsCollector(processId))
            {
                stats = statsCollector.CollectStats(CollectionTime);
            }

            new InMemoryDemoStatsHandler().HandleInMemoryDemoStats(stats);
        }

        static void HandleRedisLRU(int processId)
        {
            LRUCacheCountersStats stats;
            using (var statsCollector = new RedisLRUStatsCollector(processId))
            {
                stats = statsCollector.CollectStats(CollectionTime);
            }

            new RedisDemoStatsHandler().HandleRedisDemoStats(stats);
        }

        static void HandleNoCacheCollectDistribution(int processId)
        {
            RequestsDistributionStats stats;
            using (var statsCollector = new RequestsDistributionStatsCollector(processId))
            {
                stats = statsCollector.CollectStats(CollectionTime);
            }

            new RequestsDistributionStatsHandler().HandleRequestsDistributionStats(stats);
        }
    }
}