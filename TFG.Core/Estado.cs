using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TFG.Core.Model;

namespace TFG.Core
{
    public static class Estado
    {
        public static Uri SensoresEnPais(IEnumerable<Sensor> sensores, string pais)
        {
            var estados = sensores.Where(s => s.Pais.Equals(pais))
                .Select(s => s.StatusIcon)
                .Distinct();

            // Si alguno tiene error, mostrar icono rojo
            if (estados.Contains("Error"))
                return new Uri("https://placehold.it/24/aa0000/000000?text=+");

            // Si todos están desconectados, mostrar icono gris
            if (estados.All(n => n.Contains("Offline")))
                return new Uri("https://placehold.it/24/208020/780888?text=+");

            // En otro caso, devolver icono verde
            return new Uri("https://placehold.it/24/208020/000000?text=+");
        }
    }
}
