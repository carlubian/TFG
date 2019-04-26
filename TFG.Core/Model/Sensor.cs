using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TFG.Core.Model.SensorProperties;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace TFG.Core.Model
{
    public class Sensor
    {
        public string InternalID { get; set; }

        public string Nombre { get; set; }
        public string IP { get; set; }
        public string Puerto { get; set; }
        public string Tipo { get; set; }
        public string Pais { get; set; }
        public string Lugar { get; set; }
        public string Operaciones { get; set; }

        public ObservableCollection<TextualProperty> TextualProperties { get; set; }
        public ObservableCollection<NumericProperty> NumericProperties { get; set; }

        private SolidColorBrush _brush = new SolidColorBrush(Color.FromArgb(255, 120, 128, 136));

        public string StatusIcon
        {
            get
            {
                try
                {
                    var estado = this.TextualProperties.FirstOrDefault(p => p.Key is "Conectado");
                    if (estado is null)
                        // No hay conexión con el servidor
                        return "SensorOffline.png";

                    if (estado.Value is "True")
                        // El servidor dice que el sensor está conectado
                        return "SensorOK.png";
                    else
                        // El servidor dice que el sensor está desconectado
                        return "SensorError.png";
                }
                catch
                {
                    return "SensorOffline.png";
                }
            }
        }

        public bool Deleted { get; set; }

        public SolidColorBrush ColorEstado
        {
            get
            {
                try
                {
                    var estado = this.TextualProperties.FirstOrDefault(p => p.Key is "Conectado");
                    if (estado is null)
                        // No hay conexión con el servidor
                        this._brush = new SolidColorBrush(Color.FromArgb(255, 120, 128, 136));

                    if (estado.Value is "True")
                        // El servidor dice que el sensor está conectado
                        this._brush = new SolidColorBrush(Color.FromArgb(255, 32, 128, 32));
                    else
                        // El servidor dice que el sensor está desconectado
                        this._brush = new SolidColorBrush(Color.FromArgb(255, 170, 0, 0));
                }
                catch { }
                return this._brush;
            }
        }

        private KaomiClient Kaomi = null;
        private readonly Timer Timer;

        public Sensor()
        {
            this.Deleted = false;
            this.Timer = new Timer(_ => Parallel.Invoke(this.TimerTick), null, 0, 30000);
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
                    var textual = KaomiResponseParser.Parse(result);
                    this.TextualProperties = new ObservableCollection<TextualProperty>(textual);
                }
                else
                {

                }
            }
        }
    }
}
