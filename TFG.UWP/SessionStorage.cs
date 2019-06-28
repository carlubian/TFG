using System.Collections.ObjectModel;
using TFG.Core.Model;
using TFG.UWP.Platform;

namespace TFG.UWP
{
    /// <summary>
    /// Almacena variables de sesión compartidas
    /// entre varias páginas.
    /// </summary>
    internal static class SessionStorage
    {
        /// <summary>
        /// Lista de sensores conectados. Es preferible
        /// utilizar esta lista antes que procesar el
        /// archivo de configuración directamente.
        /// </summary>
        private static ObservableCollection<Sensor> Sensores { get; set; }
        /// <summary>
        /// Sensor en proceso de ser construido
        /// mediante los paneles de acceso rápido.
        /// </summary>
        internal static SensorBuilder SensorBeingBuilt { get; set; }

        static SessionStorage()
        {
            Sensores = new ObservableCollection<Sensor>();
            SensorBeingBuilt = null;
        }

        internal static void AddSensor(Sensor sensor)
        {
            sensor.AddListener(new SensorStatusListener());
            Sensores.Add(sensor);
        }

        internal static void RemoveSensor(Sensor sensor)
        {
            Sensores.Remove(sensor);
            sensor.ClearListeners();
        }

        internal static void ClearSensores()
        {
            foreach (var sensor in Sensores)
                sensor.ClearListeners();
            Sensores.Clear();
        }

        internal static ReadOnlyObservableCollection<Sensor> GetSensores()
        {
            return new ReadOnlyObservableCollection<Sensor>(Sensores);
        }

        internal class SensorStatusListener : ISensorStatusListener
        {
            public void OnStatusChanged(Sensor sender, SensorStatus newStatus)
            {
                Notification.Show(sender, newStatus);
            }
        }
    }
}
