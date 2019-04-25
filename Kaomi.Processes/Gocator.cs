using Kaomi.Legacy.Model;
using Lmi3d.GoSdk;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using System;
using System.Text;

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
            this.system = new GoSystem();
            this.ipAddress = KIpAddress.Parse("192.168.1.10");
            this.sensor = this.system.FindSensorByIpAddress(this.ipAddress);
            this.sensor.Connect();

            base.IterationDelay = TimeSpan.FromMinutes(1);
        }

        public override void OnFinalize() => this.sensor.Disconnect();

        public override void OnIteration()
        {
            var result = new StringBuilder("[");
            result.Append($"Modelo={this.sensor.Model}&");
            result.Append($"Compatible={this.sensor.IsCompatible()}&");
            result.Append($"Conectado={this.sensor.IsConnected()}&");
            result.Append($"Protocolo={this.sensor.ProtocolVersion.Major}.{this.sensor.ProtocolVersion.Minor}.{this.sensor.ProtocolVersion.Release}.{this.sensor.ProtocolVersion.Build}&");
            result.Append($"Firmware={this.sensor.FirmwareVersion.Major}.{this.sensor.FirmwareVersion.Minor}.{this.sensor.FirmwareVersion.Release}.{this.sensor.FirmwareVersion.Build}&");
            result.Append($"Modo de Escaneo={this.sensor.ScanMode.ToString()}&");
            result.Append($"Alineación={this.sensor.AlignmentState.ToString()}&");
            result.Append($"Estado={this.sensor.State.ToString()}&");
            result.Append($"Valor de encoder={this.sensor.Encoder()}&");

            var job = "";
            var ignore = false;
            this.sensor.LoadedJob(ref job, ref ignore);
            result.Append($"Perfil de Trabajo={job}");

            result.Append("|]");
            base.NotifyResult(result.ToString());
        }

        public override void OnUserMessage(string message)
        {

        }
    }
}
