using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace TFG.UWP.Converters
{
    public class StringToBrushConverter : IValueConverter
    {
        // Convierte "rrr:ggg:bbb" en new SolidColorBrush(Color.FromArgb(255, rrr, ggg, bbb)
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var input = value as string;
            var parts = input.Split(':').Select(s => byte.Parse(s)).ToArray();

            return new SolidColorBrush(Color.FromArgb(255, parts[0], parts[1], parts[2]));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotImplementedException();
    }
}
