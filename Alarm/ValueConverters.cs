using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace Alarm
{
    class DoubleToTimeLeftConverter : IValueConverter
    {
        // Used to display time left as a string
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double d = (double)value;
            TimeSpan t = new TimeSpan(0, 0, 0, 0, (int)d);
            int h = t.Hours;
            return (h != 0 ? t.Hours.ToString() + ":" : "") + t.Minutes.ToString().PadLeft(2, '0') + ":" + t.Seconds.ToString().PadLeft(2, '0');
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class IsGreaterThanFirstTickConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double v = (double)value;
            double p = Double.Parse(parameter.ToString());
            return v > p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class DoubleToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double d = (double)value;
            TimeSpan time = TimeSpan.FromMilliseconds(d);
            return "Time left - " + time.ToString("hh\\:mm\\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class IsGreaterThanOneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value > 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
