using AppDocumentManagment.DB.Models;
using System.Globalization;
using System.Windows.Data;

namespace AppDocumentManagment.UI.Utilities
{
    public class InternalDocumentTypeConverter : IValueConverter
    {
        public static string ConvertToString(object value)
        {
            if (value is InternalDocumentType type)
            {
                return type switch
                {
                    InternalDocumentType.Order => "Приказ",
                    InternalDocumentType.Direction => "Указание/распоряжение",
                    InternalDocumentType.Report => "Рапорт",
                    InternalDocumentType.OfficialLetter => "Служебная записка",
                };
            }
            return value.ToString();
        }

        public static InternalDocumentType ConvertToEnum(string value)
        {
            return value switch
            {
                "Приказ" => InternalDocumentType.Order,
                "Указание/распоряжение" => InternalDocumentType.Direction,
                "Рапорт" => InternalDocumentType.Report,
                "Служебная записка" => InternalDocumentType.OfficialLetter,
            };
        }

        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                InternalDocumentType.Order => 0,
                InternalDocumentType.Direction => 1,
                InternalDocumentType.Report => 2,
                InternalDocumentType.OfficialLetter => 3,
                _ => 0,
            };
        }

        public static InternalDocumentType BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => InternalDocumentType.Order,
                1 => InternalDocumentType.Direction,
                2 => InternalDocumentType.Report,
                3 => InternalDocumentType.OfficialLetter,
                _ => InternalDocumentType.Order
            };
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToString(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertToString(value);
        }
    }
}
