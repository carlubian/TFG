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

        public static KaomiClient Connect(string ip, int puerto)
        {
            return new KaomiClient
            {
                server = KaomiServer.ConnectTo(ip, puerto)
            };
        }

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

        public void AttachProcess()
        {
            process = server.AllAssemblies().AssemblyList
                .SelectMany(asm => asm.AllProcesses().ProcessList)
                .First(p => p.Id.Equals("TFG_Mock"));
        }

        public string LatestResult()
        {
            return process.GetResults().Results.Last();
        }
    }
}
