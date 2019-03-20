using System;
using System.Collections.Generic;
using System.Text;
using TFG.Core.Model.SensorProperties;

namespace TFG.Core
{
    internal static class KaomiResponseParser
    {
        internal static (IEnumerable<TextualProperty> Textual, IEnumerable<NumericProperty> Numeric) Parse(string response)
        {
            var textual = new List<TextualProperty>();
            var numeric = new List<NumericProperty>();

            // ...

            return (Textual: textual, Numeric: numeric);
        }
    }
}
