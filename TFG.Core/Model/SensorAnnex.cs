using System.Collections.Generic;
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
#pragma warning disable CA2227
        public ObservableCollection<TextualProperty> TextualProperties { get; set; }
        public ObservableCollection<NumericProperty> NumericProperties { get; set; }
#pragma warning restore CA2227

        private SolidColorBrush _brush = new SolidColorBrush(Color.FromArgb(255, 120, 128, 136));

        public bool StatusNotified { get; set; }

        private readonly IList<ISensorStatusListener> Listeners = new List<ISensorStatusListener>();

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
#pragma warning disable CA1031
                catch { }
#pragma warning restore CA1031
                return this._brush;
            }
        }

        private KaomiClient Kaomi = null;

        public void AddListener(ISensorStatusListener listener) => Listeners.Add(listener);

        public void ClearListeners() => Listeners.Clear();
    }
}
