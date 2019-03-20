using Kaomi.Legacy.Model;
using Lmi3d.GoSdk;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaomi.Processes
{
    public class Gocator : KaomiProcess
    {
        private GoSystem system = null;
        private KIpAddress ipAddress;
        private GoSensor sensor = null;

        public override void OnInitialize()
        {
            KApiLib.Construct();
            GoSdkLib.Construct();
            system = new GoSystem();
            ipAddress = KIpAddress.Parse("192.168.1.10");
            sensor = system.FindSensorByIpAddress(ipAddress);
            sensor.Connect();

            base.IterationDelay = TimeSpan.FromMinutes(1);
        }

        public override void OnFinalize()
        {
            sensor.Disconnect();
        }

        public override void OnIteration()
        {
            var result = new StringBuilder("[");
            result.Append($"Modelo={sensor.Model}&");
            result.Append($"Compatible={sensor.IsCompatible()}&");
            result.Append($"Conectado={sensor.IsConnected()}&");
            result.Append($"Protocolo={sensor.ProtocolVersion.Major}.{sensor.ProtocolVersion.Minor}.{sensor.ProtocolVersion.Release}.{sensor.ProtocolVersion.Build}&");
            result.Append($"Firmware={sensor.FirmwareVersion.Major}.{sensor.FirmwareVersion.Minor}.{sensor.FirmwareVersion.Release}.{sensor.FirmwareVersion.Build}&");
            result.Append($"Modo de Escaneo={sensor.ScanMode.ToString()}&");
            result.Append($"Alineación={sensor.AlignmentState.ToString()}&");
            result.Append($"Estado={sensor.State.ToString()}&");
            result.Append($"Valor de encoder={sensor.Encoder()}&");

            string job = "";
            bool ignore = false;
            sensor.LoadedJob(ref job, ref ignore);
            result.Append($"Perfil de Trabajo={job}");

            result.Append("|]");
            base.NotifyResult(result.ToString());
        }

        public override void OnUserMessage(string message)
        {
            
        }
    }
}
