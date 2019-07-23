using System;
using System.Collections.Generic;
using System.Linq;
using TFG.Core.Model;

namespace TFG.Core
{
    /// <summary>
    /// Hace cosas con el estado de los sensores.
    /// </summary>
    public static class Estado
    {
        /// <summary>
        /// Convierte el estado de todos los sensores de un
        /// país a una URI que lleva a la imagen coloreada
        /// correspondiente para mostrar como chincheta de mapa,
        /// </summary>
        /// <param name="sensores">Todos los sensores</param>
        /// <param name="pais">Nombre del país</param>
        /// <returns></returns>
        public static Uri SensoresEnPais(IEnumerable<Sensor> sensores, string pais)
        {
            var estados = sensores.Where(s => s.Pais.Equals(pais))
                .Select(s => s.Status)
                .Distinct();

            // Si alguno tiene error, mostrar icono rojo
            if (estados.Any(n => n is SensorStatus.Error))
                //return new Uri("https://placehold.it/24/aa0000/000000?text=+");
                return new Uri("https://carlubian.azurewebsites.net/images/SensorError.png");

            // Si todos están desconectados, mostrar icono gris
            if (estados.All(n => n is SensorStatus.Offline))
                //return new Uri("https://placehold.it/24/788088/000000?text=+");
                return new Uri("https://carlubian.azurewebsites.net/images/SensorOffline.png");
            // Si alguno está desconectado, mostrar icono amarillo
            else if (estados.Any(n => n is SensorStatus.Offline))
                //return new Uri("https://placehold.it/24/ecb300/000000?text=+");
                return new Uri("https://carlubian.azurewebsites.net/images/SensorPartial.png");

            // En otro caso, devolver icono verde
            //return new Uri("https://placehold.it/24/208020/000000?text=+");
            return new Uri("https://carlubian.azurewebsites.net/images/SensorOK.png");
        }
    }
}
