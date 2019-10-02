using Kaomi.Legacy.Model;

namespace Kaomi.Legacy.Processes
{
    /// <summary>
    /// System process that runs all startup processes
    /// as indicated in the server configuration file.
    /// </summary>
    internal class AutoStartProcesses : OneTimeProcess
    {
        private KaomiPluginConsole ServerConsole;
        private KaomiPluginConfiguration Config;

        public override void OnInitialize()
        {
            this.ServerConsole = this.Request<KaomiPluginConsole>();
            this.Config = this.Request<KaomiPluginConfiguration>();
            this.ServerConsole.WriteLine("Looking for startup processes...");
        }

        public override void DoWork()
        {
            foreach (var proc in this.Config.SettingsIn("Startup"))
            {
                try
                {
                    KaomiLoader.Load($"{proc.Key}.dll");
                }
                catch
                {
                    try
                    {
                        KaomiLoader.Load($"{proc.Key}.exe");
                    }
                    catch { }
                }
                KaomiLoader.InstanceProcess(proc.Key, proc.Value);
            }

            this.ServerConsole.WriteLine("All startup processes initialized.");
        }

        public override void OnFinalize()
        {

        }

        public override void OnUserMessage(string message)
        {

        }
    }
}
