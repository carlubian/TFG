using Kaomi.Legacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Legacy.Processes
{
    /// <summary>
    /// System process that monitors the process queue
    /// removing finished processes.
    /// </summary>
    /// <remarks>
    /// This process can access the internal field prcs
    /// in KaomiLoader, unlike user processes.
    /// </remarks>
    internal class MonitorFinishedProcesses : KaomiProcess
    {
        private KaomiPluginConsole ServerConsole;

        public override void OnInitialize()
        {
            // Iterate once every 10 seconds
            base.IterationDelay = TimeSpan.FromSeconds(10);

            // Request Kaomi Plugin
            ServerConsole = Request<KaomiPluginConsole>();
        }

        public override void OnIteration()
        {
            // These processes are either finished or about to be finalized
            var prToRemove = KaomiLoader.prcs.Where(kvp => kvp.Value.Finalize)
                .Select(kvp => kvp.Key)
                .ToArray();

            foreach (var pr in prToRemove)
            {
                KaomiLoader.prcs.Remove(pr);
                ServerConsole._WriteLine($"{pr} is being finalized and will be removed from memory.");
            }
        }

        public override void OnFinalize()
        {
            // System processes should only finalize when shutting down Kaomi
            ServerConsole._WriteLine($"System process {nameof(MonitorFinishedProcesses)} is being terminated.");
        }

        public override void OnUserMessage(string message)
        {
            // MonitorFinishedProcesses has no user commands
        }
    }
}
