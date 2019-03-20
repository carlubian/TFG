using System;
using System.Collections.Generic;
using System.Text;
using TFG.Core.Model.SensorProperties;
using Windows.UI.Xaml.Media;
using Windows.UI;
using System.Threading;
using System.Threading.Tasks;

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

        public IEnumerable<TextualProperty> TextualProperties { get; set; }
        public IEnumerable<NumericProperty> NumericProperties { get; set; }

        private KaomiClient Kaomi = null;
        private Timer Timer;

        // TODO Provisional: quitar constructor y conseguir datos desde Kaomi
        public Sensor()
        {
            Timer = new Timer(_ => Parallel.Invoke(TimerTick), null, 2000, 30000);
        }

        private void TimerTick()
        {
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
                    TextualProperties = textual;
                    NumericProperties = new List<NumericProperty>();
                }
                else
                {

                }
            }
        }
    }
}
