using System;
using System.Collections.Generic;
using System.Text;
using TFG.Core.Model.SensorProperties;
using Windows.UI.Xaml.Media;
using Windows.UI;

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

        // TODO Provisional
        public Sensor()
        {
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
    }
}
