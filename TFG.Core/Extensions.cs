using System;
using System.Collections.Generic;
using System.Text;

namespace TFG.Core
{
    public static class Extensions
    {
        /// <summary>
        /// Devuelve una cadena vacía si la
        /// cadena de entrada es null.
        /// </summary>
        /// <param name="source">Cadena de entrada</param>
        /// <returns></returns>
        public static string OrEmptyIfNull(this string source) => source is default(string) ? "" : source;
    }
}
