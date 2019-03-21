using Kaomi.Legacy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Processes
{
    public class TestGocator : KaomiProcess
    {
        private KaomiPluginConsole Console;

        public override void OnInitialize()
        {
            Console = Request<KaomiPluginConsole>();
            Console.WriteLine("[TestGocator] Este es un proceso de prueba. No debería estar presente en la versión final.",
                OutputKind.Warning);

            base.IterationDelay = TimeSpan.FromSeconds(30);
        }

        public override void OnFinalize()
        {

        }
        
        public override void OnIteration()
        {
            var random = new Random();
            var result = new StringBuilder("[");
            result.Append($"Modelo=T3ST-S3NS0R&");
            result.Append($"Compatible=True&");
            result.Append($"Conectado={(random.Next(0, 2) > 0 ? "True" : "False")}&");
            result.Append($"Protocolo=16.32.64.128&");
            result.Append($"Firmware={random.Next()}.{random.Next()}.{random.Next()}.{random.Next()}&");
            result.Append($"Modo de Escaneo=Mentira&");
            result.Append($"Alineación=Alineado&");
            result.Append($"Estado=Ready&");
            result.Append($"Valor de encoder={random.Next(1, int.MaxValue)}&");
            result.Append($"Perfil de Trabajo=FakeTestSensor.job");

            result.Append("|]");
            base.NotifyResult(result.ToString());
        }

        public override void OnUserMessage(string message)
        {

        }
    }
}
