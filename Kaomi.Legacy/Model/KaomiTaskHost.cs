using System;
using System.Collections.Generic;
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
            this.AssemblyId = assemblyId;
            this.Process = process;
            this.Finalize = false;
            this.UserCommand = new Queue<string>();
            this.Results = new Queue<string>();
            process.TaskHost = this;

            this.ScaffoldAndRun();
        }

        /// <summary>
        /// Creates the wrapper task and starts it.
        /// </summary>
        private void ScaffoldAndRun()
        {
            this.Task = new Task(this.RunProcess, TaskCreationOptions.LongRunning);
            this.Task.Start();
        }

        /// <summary>
        /// Logic to run a KaomiProcess. This method will run inside a Task.
        /// </summary>
        private void RunProcess()
        {
            // Initialize the process
            this.Process.OnInitialize();

            // Main process loop
            while (!this.Finalize)
            {
                // One-Time processes should iterate only once
                if (this.Process is OneTimeProcess)
                    this.Finalize = true;

                this.CheckUserMessages();

                // Do an iteration
                this.Process.OnIteration();

                if (this.Process.RequestFinalization)
                    this.Finalize = true;

                // Wait for process iteration delay
                if (this.Process.IterationDelay <= TimeSpan.FromSeconds(60))
                    this.WaitForIteration(this.Process.IterationDelay);
                else
                {
                    // Break long intervals in several one minute pauses
                    var interval = this.Process.IterationDelay;

                    while (interval.TotalMinutes > 0)
                    {
                        if (interval.TotalMinutes < 1)
                            this.WaitForIteration(interval);
                        else
                            this.WaitForIteration(TimeSpan.FromMinutes(1));

                        interval -= TimeSpan.FromMinutes(1);

                        this.CheckUserMessages();

                        if (this.Process.RequestFinalization)
                            this.Finalize = true;
                    }
                }
            }

            // Finalize the process
            this.Process.OnFinalize();
        }

        private void CheckUserMessages()
        {
            if (this.UserCommand.Count > 0)
                this.Process.OnUserMessage(this.UserCommand.Dequeue());
        }

        private void WaitForIteration(TimeSpan timeSpan) => Thread.Sleep(Convert.ToInt32(timeSpan.TotalMilliseconds));
    }
}
