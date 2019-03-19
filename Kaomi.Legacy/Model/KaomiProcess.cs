using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Legacy.Model
{
    /// <summary>
    /// Represents a process that will run continuously
    /// on a Kaomi Task Host.
    /// </summary>
    public abstract class KaomiProcess : MarshalByRefObject
    {
        public bool RequestFinalization { get; set; } = false;
        internal KaomiTaskHost TaskHost { get; set; }

        public TimeSpan IterationDelay { get; set; } = TimeSpan.FromSeconds(1);

        public T Request<T>() where T : KaomiPlugin, new()
        {
            var plugin = new T();
            plugin.Initialize(Assembly.GetCallingAssembly().GetName().Name);
            return plugin;
        }

        public void NotifyResult(string result)
        {
            // Limit the amount of results per process
            if (TaskHost.Results.Count > 200)
                TaskHost.Results.Dequeue();
            TaskHost.Results.Enqueue(result);
        }

        public abstract void OnInitialize();

        public abstract void OnIteration();

        public abstract void OnUserMessage(string message);

        public abstract void OnFinalize();
    }
}
