using System.Collections.Generic;
using TFG.Core.Model.SensorProperties;

namespace TFG.Core
{
    internal static class KaomiResponseParser
    {
        internal static (IEnumerable<TextualProperty> textual, IEnumerable<NumericProperty> numeric) Parse(string response)
        {
            response.Replace("[", "");
            response.Replace("]", "");

            var categorias = response.Split('|');

            // Ajustes textuales
            var ajtex = categorias[0].Split('&');
            var textual = new List<TextualProperty>();

            foreach (var ajuste in ajtex)
            {
                var partes = ajuste.Split('=');
                textual.Add(new TextualProperty
                {
                    Key = partes[0].Replace("[", ""),
                    Value = partes[1],
                    Color = GetForeground(partes[0], partes[1])
                });
            }

            // Ajustes numéricos
            var ajnum = categorias[1].Split('&');
            var numeric = new List<NumericProperty>();

            return (textual, numeric);
        }

        private static string GetForeground(string key, string value)
        {
            // ¿Es la propiedad 'Conectado'?
            if (key is "Conectado")
                return value is "True"
                    ? "32:128:32"
                    : "170:0:0";
            // ¿Es la propiedad 'Compatible'?
            if (key is "Compatible")
                return value is "True"
                    ? "32:128:32"
                    : "170:0:0";
            // ¿Es la propiedad 'Estado'?
            if (key is "Estado")
                return value is "Ready"
                    ? "235:193:0"
                    : "32:128:32";

            // Ante la duda, ponerlo en blanco
            return "245:250:255";
        }
    }
}
