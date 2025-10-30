using AppDocumentManagment.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocumentManagment.UI.Utilities
{
    public class InternalDocumentTypeConverter
    {
        public static string ConvertToString(object value)
        {
            if (value is InternalDocumentTypes type)
            {
                return type switch
                {
                    InternalDocumentTypes.Order => "Приказ",
                    InternalDocumentTypes.Direction => "Указание/распоряжение",
                    InternalDocumentTypes.Report => "Рапорт",
                    InternalDocumentTypes.OfficialLetter => "Служебная записка",
                };
            }
            return value.ToString();
        }

        public static InternalDocumentTypes ConvertToEnum(string value)
        {
            return value switch
            {
                "Приказ" => InternalDocumentTypes.Order,
                "Указание/распоряжение" => InternalDocumentTypes.Direction,
                "Рапорт" => InternalDocumentTypes.Report,
                "Служебная записка" => InternalDocumentTypes.OfficialLetter,
            };
        }

        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                InternalDocumentTypes.Order => 0,
                InternalDocumentTypes.Direction => 1,
                InternalDocumentTypes.Report => 2,
                InternalDocumentTypes.OfficialLetter => 3,
                _ => 0,
            };
        }

        public static InternalDocumentTypes BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => InternalDocumentTypes.Order,
                1 => InternalDocumentTypes.Direction,
                2 => InternalDocumentTypes.Report,
                3 => InternalDocumentTypes.OfficialLetter,
                _ => InternalDocumentTypes.Order
            };
        }
    }
}
