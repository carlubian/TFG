using System.Collections.ObjectModel;
using System.Linq;
using TFG.Core.Model.SensorProperties;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace TFG.Core.Model
{
    // Cosas que pertenecen a Sensor pero distraen.
    public partial class Sensor
    {
        public ObservableCollection<TextualProperty> TextualProperties { get; set; }
        public ObservableCollection<NumericProperty> NumericProperties { get; set; }

        private SolidColorBrush _brush = new SolidColorBrush(Color.FromArgb(255, 120, 128, 136));

        public string StatusIcon
        {
            get
            {
                if (Status is SensorStatus.Offline)
                    return "SensorOffline.png";

                if (Status is SensorStatus.Online)
                    return "SensorOK.png";

                else
                    return "SensorError.png";
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
    }
}
