using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TeamKits.Converters
{
    public class StringColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || value.GetType() != typeof(string)) return new SolidColorBrush(Colors.Transparent);
            var color = (SolidColorBrush)new BrushConverter().ConvertFrom(value);
            if (color == null) return new SolidColorBrush(Colors.Transparent);
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
