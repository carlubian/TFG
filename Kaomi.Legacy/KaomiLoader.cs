using Kaomi.Legacy.Model;
using Kaomi.Legacy.Processes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Legacy
{
    /// <summary>
    /// Entry point for all server related Kaomi operations.
    /// </summary>
    public static class KaomiLoader
    {
        internal static IDictionary<string, Assembly> asms = new Dictionary<string, Assembly>();
        internal static IDictionary<string, KaomiTaskHost> prcs = new Dictionary<string, KaomiTaskHost>();

        static KaomiLoader()
        {
            // Start system processes
            prcs.Add("MonitorFinishedProcesses", new KaomiTaskHost("System", new MonitorFinishedProcesses()));
            prcs.Add("AutoStartProcesses", new KaomiTaskHost("System", new AutoStartProcesses()));
        }

        /// <summary>
        /// Checks whether the server is active.
        /// Also initializes it if that hasn't
        /// happened already.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static bool IsActive() => true;

        /// <summary>
        /// Tells the server to download a file from the
        /// specified URI into its local directory.
        /// </summary>
        /// <param name="fileName">Name of the file</param>
        /// <param name="uri">Path to the file</param>
        public static void PullFromUri(string fileName, Uri uri)
        {
            using (var web = new WebClient())
                web.DownloadFile(uri, fileName);
        }

        /// <summary>
        /// Tells the server to load an assembly from
        /// its local directory to active memory.
        /// </summary>
        /// <param name="path">Name of the assembly, 
        /// including file extension</param>
        /// <returns></returns>
        public static string Load(string path)
        {
            Assembly asm;
            var bytes = File.ReadAllBytes(path);
            asm = Assembly.Load(bytes);

            var id = asm.GetName().Name;

            if (asms.ContainsKey(id))
                asms.Remove(id);

            asms.Add(id, asm);

            return id;
        }

        /// <summary>
        /// Lists all loaded assemblies from the server.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> List()
        {
            return asms.Keys;
        }

        /// <summary>
        /// Tells the server to unload an assembly from
        /// its memory, finishing all of its processes.
        /// </summary>
        /// <param name="id">Assembly name</param>
        public static void Unload(string id)
        {
            var asm = asms[id];

            if (asm is null)
                return;

            asms.Remove(id);

            // Assembly unloading is only supported on .NET Core
            //AssemblyLoadContext.GetLoadContext(asm).Unload();
        }

        /// <summary>
        /// Tells the server to run a process from one
        /// of its loaded assemblies.
        /// </summary>
        /// <param name="id">Assembly name</param>
        /// <param name="type">Process name</param>
        public static void InstanceProcess(string id, string type)
        {
            var asm = asms[id];

            if (asm is null)
                return;

            var msg = asm.GetTypes().First(t => t.Name.Contains(type));
            var obj = Activator.CreateInstance(msg) as KaomiProcess;

            prcs.Add(type, new KaomiTaskHost(id, obj));
        }

        /// <summary>
        /// Lists all currently running processes
        /// from the server.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> ListProcesses()
        {
            return prcs.Select(kvp => $"[{kvp.Value.AssemblyId}] {kvp.Key}");
        }

        /// <summary>
        /// Checks whether a process has available results.
        /// </summary>
        /// <param name="processId">Process name</param>
        /// <returns></returns>
        public static bool HasResults(string processId)
        {
            return prcs[processId].Results.Count > 0;
        }

        /// <summary>
        /// Gets all the pending results for a process,
        /// removing them from the result queue.
        /// </summary>
        /// <param name="processId">Process name</param>
        /// <returns></returns>
        public static IEnumerable<string> GetResults(string processId)
        {
            while (prcs[processId].Results.Count > 0)
                yield return prcs[processId].Results.Dequeue();
        }

        /// <summary>
        /// Sends a message to one of the active processes.
        /// </summary>
        /// <param name="processId">Process name</param>
        /// <param name="message">Text message</param>
        public static void SendMessage(string processId, string message)
        {
            if (prcs[processId].UserCommand.Count > 200)
                prcs[processId].UserCommand.Dequeue();
            prcs[processId].UserCommand.Enqueue(message);
        }
    }
}
