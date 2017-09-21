using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Mosaic.UI.Converters
{
    public class ZeroToEmptyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Equals(value.GetType(), typeof(int))) return null;
            if ((int)value == 0) return value = string.Empty;
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!Equals(value.GetType(), typeof(string))) return null;
            if ((string)value == "0") return value = string.Empty;
            return value;
        }
    }
}
