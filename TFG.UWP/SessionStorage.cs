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
        internal static ObservableCollection<Sensor> Sensores { get; set; }

        static SessionStorage()
        {
            Sensores = new ObservableCollection<Sensor>();
        }
    }
}
