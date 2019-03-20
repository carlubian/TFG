using System;
using System.Collections.Generic;
using System.Text;
using TFG.Core.Model.SensorProperties;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace TFG.Core
{
    internal static class KaomiResponseParser
    {
        internal static IEnumerable<TextualProperty> Parse(string response)
        {
            response.Replace("[", "");
            response.Replace("]", "");

            var categorias = response.Split('|');

            // Ajustes textuales
            var ajtex = categorias[0].Split('&');

            foreach (var ajuste in ajtex)
            {
                var partes = ajuste.Split('=');
                yield return new TextualProperty
                {
                    Key = partes[0],
                    Value = partes[1],
                    Color = GetForeground(partes[0], partes[1])
                };
            }
        }

        private static SolidColorBrush GetForeground(string key, string value)
        {
            // ¿Es la propiedad 'Conectado'?
            if (key is "Conectado")
                return value is "True" 
                    ? new SolidColorBrush(Color.FromArgb(255, 32, 128, 32))
                    : new SolidColorBrush(Color.FromArgb(255, 170, 0, 0));
            // ¿Es la propiedad 'Compatible'?
            if (key is "Compatible")
                return value is "True"
                    ? new SolidColorBrush(Color.FromArgb(255, 32, 128, 32))
                    : new SolidColorBrush(Color.FromArgb(255, 170, 0, 0));
            // ¿Es la propiedad 'Estado'?
            if(key is "Estado")
                return value is "Ready"
                    ? new SolidColorBrush(Color.FromArgb(255, 235, 193, 0))
                    : new SolidColorBrush(Color.FromArgb(255, 32, 128, 32));

            // Ante la duda, ponerlo en blanco
            return new SolidColorBrush(Color.FromArgb(255, 245, 250, 255));
        }
    }
}
