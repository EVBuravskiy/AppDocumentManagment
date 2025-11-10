using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.UI.Utilities
{
    public class DocumentStatusConverter
    {
        public static string ConvertToString(object value)
        {
            if (value is DocumentStatus externalDocumentStatus)
            {
                return externalDocumentStatus switch
                {
                    DocumentStatus.UnderConsideration => "На рассмотрении",
                    DocumentStatus.Agreed => "Согласованo",
                    DocumentStatus.Refused => "Отказанo"
                };
            }
            return value.ToString();
        }

        public static DocumentStatus ConvertToEnum(string value)
        {
            return value switch
            {
                "На рассмотрении" => DocumentStatus.UnderConsideration,
                "Согласованo" => DocumentStatus.Agreed,
                "Отказанo" => DocumentStatus.Refused,
            };
        }

        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                DocumentStatus.UnderConsideration => 0,
                DocumentStatus.Agreed => 1,
                DocumentStatus.Refused => 2,
                _ => 0,
            };
        }

        public static DocumentStatus BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => DocumentStatus.UnderConsideration,
                1 => DocumentStatus.Agreed,
                2 => DocumentStatus.Refused,
            };
        }
    }
}
