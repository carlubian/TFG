using System.Collections.ObjectModel;
using TFG.Core.Model;

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
        internal static ObservableCollection<Sensor> Sensores { get; set; }
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
    }
}
