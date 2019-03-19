using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kaomi.Legacy.Model
{
    internal class KaomiTaskHost
    {
        internal string AssemblyId { get; set; }
        internal KaomiProcess Process { get; set; }
        internal bool Finalize { get; set; }
        internal Queue<string> UserCommand { get; set; }
        internal Queue<string> Results { get; set; }

        private Task Task { get; set; }

        /// <summary>
        /// Create a new Task Host object.
        /// </summary>
        /// <param name="assemblyId">ID of the assembly that contains the type to run.</param>
        /// <param name="process">Instance of KaomiProcessd</param>
        public KaomiTaskHost(string assemblyId, KaomiProcess process)
        {
            AssemblyId = assemblyId;
            Process = process;
            Finalize = false;
            UserCommand = new Queue<string>();
            Results = new Queue<string>();
            process.TaskHost = this;

            ScaffoldAndRun();
        }

        /// <summary>
        /// Creates the wrapper task and starts it.
        /// </summary>
        private void ScaffoldAndRun()
        {
            Task = new Task(RunProcess, TaskCreationOptions.LongRunning);
            Task.Start();
        }

        /// <summary>
        /// Logic to run a KaomiProcess. This method will run inside a Task.
        /// </summary>
        private void RunProcess()
        {
            // Initialize the process
            Process.OnInitialize();

            // Main process loop
            while (!Finalize)
            {
                // One-Time processes should iterate only once
                if (Process is OneTimeProcess)
                    Finalize = true;

                CheckUserMessages();

                // Do an iteration
                Process.OnIteration();

                if (Process.RequestFinalization)
                    Finalize = true;

                // Wait for process iteration delay
                if (Process.IterationDelay <= TimeSpan.FromSeconds(60))
                    WaitForIteration(Process.IterationDelay);
                else
                {
                    // Break long intervals in several one minute pauses
                    var interval = Process.IterationDelay;

                    while (interval.TotalMinutes > 0)
                    {
                        if (interval.TotalMinutes < 1)
                            WaitForIteration(interval);
                        else
                            WaitForIteration(TimeSpan.FromMinutes(1));

                        interval -= TimeSpan.FromMinutes(1);

                        CheckUserMessages();

                        if (Process.RequestFinalization)
                            Finalize = true;
                    }
                }
            }

            // Finalize the process
            Process.OnFinalize();
        }

        private void CheckUserMessages()
        {
            if (UserCommand.Count > 0)
                Process.OnUserMessage(UserCommand.Dequeue());
        }

        private void WaitForIteration(TimeSpan timeSpan)
        {
            Thread.Sleep(Convert.ToInt32(timeSpan.TotalMilliseconds));
        }
    }
}
