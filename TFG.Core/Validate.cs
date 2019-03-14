using DotNet.Misc.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFG.Core
{
    /// <summary>
    /// Ofrece funciones de validación
    /// para las entradas de usuario.
    /// </summary>
    public static class Validate
    {
        /// <summary>
        /// Comprueba que una dirección IP
        /// tenga el formato adecuado.
        /// </summary>
        /// <param name="ip">Texto de entrada</param>
        /// <returns></returns>
        public static bool IPAddress(string ip)
        {
            if (ip is default(string))
                return false;

            var pieces = ip.Split('.'.Enumerate().ToArray(), StringSplitOptions.RemoveEmptyEntries);

            if (pieces.Length != 4)
                return false;

            return pieces.All(n => byte.TryParse(n, out var _));
        }

        /// <summary>
        /// Comprueba que un puerto tenga
        /// el formato adecuado.
        /// </summary>
        /// <param name="port">Texto de entrada</param>
        /// <returns></returns>
        public static bool Port(string port)
        {
            if (port is default(string))
                return false;

            if (int.TryParse(port, out var ptn))
                return ptn > 0;

            return false;
        }

        /// <summary>
        /// Comprueba que en un endpoint indicado
        /// a través de su dirección IP y puerto
        /// haya un servidor remoto a la escucha.
        /// </summary>
        /// <param name="ip">Dirección IP</param>
        /// <param name="port">Puerto</param>
        /// <returns></returns>
        public static bool ServerAt(string ip, string port)
        {
            if (!IPAddress(ip) || !Port(port))
                return false;

            // TODO
            return true;
        }

        /// <summary>
        /// Comprueba que un string no es
        /// nulo y contiene al menos un carácter.
        /// </summary>
        /// <param name="input">Texto de entrada</param>
        /// <returns></returns>
        public static bool StringContainsText(string input)
        {
            if (input is default(string))
                return false;

            return input.Length > 0;
        }
    }
}
