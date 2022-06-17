using System.Diagnostics;
using Microsoft.Diagnostics.NETCore.Client;

namespace CountersMonitor
{
    /// <summary>
    /// Code fetching dotnet processes was copied from/inspired by `dotnet-counters` tool source code. 
    /// This class repeats some code from 'ps' command.
    /// Source code could be found in this repository: https://github.com/dotnet/diagnostics/tree/main/src/Tools/dotnet-counters
    /// </summary>
    public class ProcessFetcher
    {
        private const string LRUInMemoryCache = "LRUInMemoryCache";
        private const string RedisLRUCache = "RedisLRUCache";
        private static readonly string[] SupposedProcessNames = new[]
        {
            LRUInMemoryCache,
            RedisLRUCache
        };

        public static readonly ProcessFetcher Instance = new ProcessFetcher();

        private ProcessFetcher() { }

        public (int processId, DemoTypes demoType) GetDemoProcess()
        {
            var currentPid = Process.GetCurrentProcess().Id;

            var processes = DiagnosticsClient.GetPublishedProcesses()
                    .Where(pid => pid != currentPid)
                    .Select(GetProcessById)
                    .Where(process => process != null)
                    .ToList();

            if (processes.Count == 0)
                throw new ApplicationException("No supported .NET processes were found");

            var supposedProcesses = processes
                .Where(x => SupposedProcessNames.Any(pn => x!.ProcessName.Contains(pn)))
                .ToList();

            if (supposedProcesses.Count == 0)
                throw new ApplicationException("No demo processes were found. Run either in-memory lru or redis lru ASP NET Core demo application !");

            if (supposedProcesses.Count > 1)
                throw new ApplicationException("Conflicting demo applications are running. Please, terminate one of demo processes to collect statistics !");

            var process = supposedProcesses.Single();
            Console.WriteLine($"Found process '{process!.ProcessName}' with ProcessID:{process!.Id}");
            Console.WriteLine(new string('-', 30));

            var demoType = process!.ProcessName.Contains(LRUInMemoryCache) ?
                DemoTypes.InMemoryLRU :
                DemoTypes.RedisLRU;

            return (process!.Id, demoType);
        }

        private Process? GetProcessById(int processId)
        {
            try
            {
                return Process.GetProcessById(processId);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }
    }
}
