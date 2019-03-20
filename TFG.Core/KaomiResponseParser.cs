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
                    Color = new SolidColorBrush(Color.FromArgb(255, 235, 193, 0))
                };
            }
        }
    }
}
