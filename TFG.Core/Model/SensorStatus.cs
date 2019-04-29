using System;
using System.Collections.Generic;
using System.Text;

namespace TFG.Core.Model
{
    /// <summary>
    /// Indica el estado de un sensor.
    /// </summary>
    public enum SensorStatus
    {
        /// <summary>
        /// El sensor está conectado y funcionando.
        /// </summary>
        Online,
        /// <summary>
        /// El sensor está desconectado.
        /// </summary>
        Offline,
        /// <summary>
        /// El sensor está conectado pero tiene un error.
        /// </summary>
        Error
    }
}
