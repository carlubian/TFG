using DotNet.Misc.Extensions.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFG.Core
{
    public static class Validate
    {
        public static bool IPAddress(string ip)
        {
            if (ip is default(string))
                return false;

            var pieces = ip.Split('.'.Enumerate().ToArray(), StringSplitOptions.RemoveEmptyEntries);

            if (pieces.Length != 4)
                return false;

            return pieces.All(n => byte.TryParse(n, out var _));
        }

        public static bool Port(string port)
        {
            if (port is default(string))
                return false;

            if (int.TryParse(port, out var ptn))
                return ptn > 0;

            return false;
        }

        public static bool ServerAt(string ip, string port)
        {
            if (!IPAddress(ip) || !Port(port))
                return false;

            // TODO
            return true;
        }
    }
}
