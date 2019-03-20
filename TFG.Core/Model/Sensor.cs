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
            Timer = new Timer(_ => Parallel.Invoke(TimerTick), null, 2000, 60000);

            TextualProperties = new List<TextualProperty>
            {
                new TextualProperty
                {
                    Key = "Conectado",
                    Value = "Sí",
                    Color = new SolidColorBrush(Color.FromArgb(255, 64, 160, 64))
                },
                new TextualProperty
                {
                    Key = "Leyendo perfiles",
                    Value = "No",
                    Color = new SolidColorBrush(Color.FromArgb(255, 218, 47, 47))
                },
                new TextualProperty
                {
                    Key = "Recibiendo superficie",
                    Value = "No",
                    Color = new SolidColorBrush(Color.FromArgb(255, 218, 47, 47))
                }
            };

            NumericProperties = new List<NumericProperty>
            {
                new NumericProperty
                {
                    Min = 0,
                    Max = 100,
                    Value = 65,
                    Tick = 20,
                    Caption = "Frecuencia"
                },
                new NumericProperty
                {
                    Min = 0,
                    Max = 1000,
                    Value = 250,
                    Tick = 200,
                    Caption = "Vel. de red"
                },
                new NumericProperty
                {
                    Min = 0,
                    Max = 1040,
                    Value = 1040,
                    Tick = 200,
                    Caption = "Anchura"
                },
                new NumericProperty
                {
                    Min = 0,
                    Max = 4,
                    Value = 1,
                    Tick = 1,
                    Caption = "Otro valor"
                }
            };
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
                    (var textual, var numeric) = KaomiResponseParser.Parse(result);
                    TextualProperties = textual;
                    NumericProperties = numeric;
                }
                else
                {

                }
            }
        }
    }
}
