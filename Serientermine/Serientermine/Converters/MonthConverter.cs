using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Serientermine.Converters
{
    public class MonthConverter : IValueConverter
    {
        private MonthConverter() { }

        public static MonthConverter Instance
            => new();

        public int ZeroValue { get; set; } = 0;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //value Wert der Eigenschaft die rein kommt
            //return Wert der Eigenschaft an die gebunden wird Type beachten
            if(value is double d)
            {
                return GetMonthName(d);
            }

            return string.Empty;
        }


        private string GetMonthName(double monthNumber)
        {
            // Gibt den Namen des Monats für die gegebene Monatsnummer zurück
            return new DateTime(2000, System.Convert.ToInt32( monthNumber), 1).ToString("MMMM");
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => DependencyProperty.UnsetValue;
    }
}
