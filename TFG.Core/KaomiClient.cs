using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Kaomi.Client;

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
            return new KaomiClient
            {
                server = KaomiServer.ConnectTo(ip, puerto)
            };
        }

        /// <summary>
        /// Checks whether a server is actually connected.
        /// </summary>
        /// <returns></returns>
        public bool Connected()
        {
            try
            {
                return server.IsListening();
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
        public void AttachProcess(string pid)
        {
            process = server.AllAssemblies().AssemblyList
                .SelectMany(asm => asm.AllProcesses().ProcessList)
                .First(p => p.Id.ToUpperInvariant().Equals(pid.ToUpperInvariant()));
        }

        /// <summary>
        /// Checks whether the process has results available.
        /// </summary>
        /// <returns></returns>
        public bool? HasResults()
        {
            return process?.HasResults().HasResults;
        }

        /// <summary>
        /// Returns the latest result of the process.
        /// </summary>
        /// <returns></returns>
        public string LatestResult()
        {
            return process?.GetResults().Results.Last();
        }
    }
}
