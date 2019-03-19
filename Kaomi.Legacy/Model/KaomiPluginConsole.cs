using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            fore = ConsoleColor.Gray;
            back = ConsoleColor.Black;
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

            Console.ForegroundColor = fore;
            Console.BackgroundColor = back;
        }

        internal void _WriteLine(object content)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            Console.WriteLine($"[SYSTEM] {content.ToString()}");

            Console.ForegroundColor = fore;
            Console.BackgroundColor = back;
        }
    }

    public enum OutputKind
    {
        Info,
        Warning,
        Error
    }
}
