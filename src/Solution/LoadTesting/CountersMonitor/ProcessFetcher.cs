using System.Diagnostics;
using Microsoft.Diagnostics.NETCore.Client;

namespace CountersMonitor
{
    // TODO add note where it was copied from
    public class ProcessFetcher
    {
        public static readonly string[] SupposedProcessNames = new[]
        {
            "LRUInMemoryCache",
            "RedisLRUCache"
        };

        public static readonly ProcessFetcher Instance = new ProcessFetcher();

        private ProcessFetcher() { }

        public (int processId, string processName) GetDemoProcess()
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

            return (process!.Id, process!.ProcessName);
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
