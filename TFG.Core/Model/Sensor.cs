using System;
using System.Collections.Generic;
using System.Text;
using TFG.Core.Model.SensorProperties;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;

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

        public bool Deleted { get; set; }

        public SolidColorBrush ColorEstado
        {
            get
            {
                try
                {
                    var estado = TextualProperties.FirstOrDefault(p => p.Key is "Conectado");
                    if (estado is null)
                        // No hay conexión con el servidor
                        _brush = new SolidColorBrush(Color.FromArgb(255, 120, 128, 136));

                    if (estado.Value is "True")
                        // El servidor dice que el sensor está conectado
                        _brush = new SolidColorBrush(Color.FromArgb(255, 32, 128, 32));
                    else
                        // El servidor dice que el sensor está desconectado
                        _brush = new SolidColorBrush(Color.FromArgb(255, 170, 0, 0));
                }
                catch { }
                return _brush;
            }
        }

        private KaomiClient Kaomi = null;
        private Timer Timer;

        public Sensor()
        {
            Deleted = false;
            Timer = new Timer(_ => Parallel.Invoke(TimerTick), null, 2000, 30000);
            TextualProperties = new ObservableCollection<TextualProperty>();
            NumericProperties = new ObservableCollection<NumericProperty>();
        }

        private void TimerTick()
        {
            if (Deleted is true)
                return;
            if (Kaomi is null)
                Kaomi = KaomiClient.Connect(IP, int.Parse(Puerto));

            if (Kaomi.Connected())
            {
                // El proceso debe llamarse como el tipo del sensor
                Kaomi.AttachProcess(Tipo);

                if (Kaomi.HasResults() is true)
                {
                    var result = Kaomi.LatestResult();
                    var textual = KaomiResponseParser.Parse(result);
                    TextualProperties = new ObservableCollection<TextualProperty>(textual);
                }
                else
                {

                }
            }
        }
    }
}
