using Kaomi.Client;
using System.Linq;

namespace TFG.Core
{
    /// <summary>
    /// TODO Implementación provisional.
    /// </summary>
    public class KaomiClient
    {
        private KaomiServer server;
        private KaomiProcess process;

        /// <summary>
        /// Se conecta a un servidor Kaomi.
        /// </summary>
        /// <param name="ip">Dirección IP</param>
        /// <param name="puerto">Puerto</param>
        /// <returns></returns>
        public static KaomiClient Connect(string ip, int puerto)
        {
            try
            {
                return new KaomiClient
                {
                    server = KaomiServer.ConnectTo(ip, puerto)
                };
            }
            catch
            {
                return new KaomiClient();
            }
        }

        /// <summary>
        /// Checks whether a server is actually connected.
        /// </summary>
        /// <returns></returns>
        public bool Connected()
        {
            try
            {
                return this.server.IsListening();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Finds and attaches to a process with
        /// the given ID (case invariant).
        /// </summary>
        /// <param name="pid">Process ID</param>
        public void AttachProcess(string pid) => this.process = this.server.AllAssemblies().AssemblyList
                .SelectMany(asm => asm.AllProcesses().ProcessList)
                .FirstOrDefault(p => p.Id.ToUpperInvariant().Equals(pid.ToUpperInvariant()));

        /// <summary>
        /// Checks whether the process has results available.
        /// </summary>
        /// <returns></returns>
        public bool? HasResults() => this.process?.HasResults().HasResults;

        /// <summary>
        /// Returns the latest result of the process.
        /// </summary>
        /// <returns></returns>
        public string LatestResult() => this.process?.GetResults().Results.Last();
    }
}
