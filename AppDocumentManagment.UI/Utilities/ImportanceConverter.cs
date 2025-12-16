using System.Globalization;
using System.Windows.Data;

namespace AppDocumentManagment.UI.Utilities
{
    [ValueConversion(typeof(String), typeof(Boolean))]
    public class ImportanceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (value is bool importance)
                {
                    if (importance == true)
                    {
                        value = "ВАЖНО!!!";
                    }
                    else
                    {
                        value = string.Empty;
                    }
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            bool importance = false;
            if (strValue != null)
            {
                if (strValue == "ВАЖНО!!!") { importance = true; }
                else { importance = false; }
            }
            return importance;
        }
    }
}
