using System;

namespace Kaomi.Legacy.Model
{
    /// <summary>
    /// Allows processes to print coloured messages
    /// in the server Kestrel console.
    /// </summary>
    public class KaomiPluginConsole : KaomiPlugin
    {
        ConsoleColor fore, back;

        public override void Initialize(string callingAssembly)
        {
            this.fore = ConsoleColor.Gray;
            this.back = ConsoleColor.Black;
        }

        public void WriteLine(object content, OutputKind kind = OutputKind.Info)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            switch (kind)
            {
                case OutputKind.Info:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case OutputKind.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case OutputKind.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine(content.ToString());

            Console.ForegroundColor = this.fore;
            Console.BackgroundColor = this.back;
        }

        internal void WriteLine(object content)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine($"[SYSTEM] {content.ToString()}");

            Console.ForegroundColor = this.fore;
            Console.BackgroundColor = this.back;
        }
    }

    public enum OutputKind
    {
        Info,
        Warning,
        Error
    }
}
