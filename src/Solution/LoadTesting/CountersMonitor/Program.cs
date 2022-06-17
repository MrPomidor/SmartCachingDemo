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
            (var processId, var demoType) = ProcessFetcher.Instance.GetDemoProcess();

            CountersStats stats;
            using (var statsCollector = new StatsCollector(processId))
            {
                stats = statsCollector.CollectStats(CollectionTime);
            }

            StatsHandler.Instance.Handle(stats, demoType);
        }
    }
}