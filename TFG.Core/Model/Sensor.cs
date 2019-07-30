using DotNet.Misc.Extensions.Linq;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TFG.Core.Model.SensorProperties;

namespace TFG.Core.Model
{
    // Parte principal de la clase Sensor.
    public partial class Sensor
    {
        public string InternalID { get; set; }

        public string Nombre { get; set; }
        public string IP { get; set; }
        public string Puerto { get; set; }
        public string Tipo { get; set; }
        public string Pais { get; set; }
        public string Lugar { get; set; }
        public string Operaciones { get; set; }

        private int intentos;

        public SensorStatus Status
        {
            get
            {
                try
                {
                    var estado = this.TextualProperties.FirstOrDefault(p => p.Key is "Conectado");
                    if (estado is null)
                    {
                        if (StatusNotified is false)
                        {
                            StatusNotified = true;
                            Listeners.ForEach(l => l.OnStatusChanged(this, SensorStatus.Offline));
                        }
                        // No hay conexión con el servidor
                        return SensorStatus.Offline;
                    }

                    if (estado.Value is "True")
                    {
                        if (StatusNotified is true)
                            StatusNotified = false;
                        // El servidor dice que el sensor está conectado
                        return SensorStatus.Online;
                    }
                    else
                    {
                        if (StatusNotified is false)
                        {
                            StatusNotified = true;
                            Listeners.ForEach(l => l.OnStatusChanged(this, SensorStatus.Error));
                        }
                        // El servidor dice que el sensor está desconectado
                        return SensorStatus.Error;
                    }
                }
                catch
                {
                    return SensorStatus.Offline;
                }
            }
        }

        public Sensor(int intentos, int delay)
        {
            // Validar parámetros
            if (intentos <= 0 || intentos > 10)
                throw new ArgumentException("Número de intentos incorrecto [0..10]", nameof(intentos));
            if (delay < 10 || delay > 600)
                throw new ArgumentException("Intervalo de actualización incorrecto [10..600]", nameof(delay));

            this.intentos = intentos;
            this.Deleted = false;
            this.StatusNotified = false;
            new Timer(_ => Parallel.Invoke(this.TimerTick), null, 0, delay * 1000);
            this.TextualProperties = new ObservableCollection<TextualProperty>();
            this.NumericProperties = new ObservableCollection<NumericProperty>();
        }

        private void TimerTick()
        {
            if (this.Deleted is true)
                return;
            if (this.Kaomi is null)
                try
                {
                    this.Kaomi = KaomiClient.Connect(this.IP, int.Parse(this.Puerto));
                }
                catch
                {
                    return;
                }

            if (this.Kaomi.Connected())
            {
                // El proceso debe llamarse como el tipo del sensor
                this.Kaomi.AttachProcess(this.Tipo);

                if (this.Kaomi.HasResults() is true)
                {
                    var result = this.Kaomi.LatestResult();
                    var (textual, numeric) = KaomiResponseParser.Parse(result);
                    this.TextualProperties = new ObservableCollection<TextualProperty>(textual);
                    this.NumericProperties = new ObservableCollection<NumericProperty>(numeric);
                }
                else
                {

                }
            }
        }
    }
}
