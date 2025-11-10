using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.UI.Utilities
{
    public class ExternalDocumentStatusConverter
    {
        public static string ConvertToString(object value)
        {
            if (value is ExternalDocumentStatus externalDocumentStatus)
            {
                return externalDocumentStatus switch
                {
                    ExternalDocumentStatus.UnderConsideration => "На рассмотрении",
                    ExternalDocumentStatus.Considered => "Рассмотрен",
                };
            }
            return value.ToString();
        }

        public static ExternalDocumentStatus ConvertToEnum(string value)
        {
            return value switch
            {
                "На рассмотрении" => ExternalDocumentStatus.UnderConsideration,
                "Рассмотрен" => ExternalDocumentStatus.Considered,
            };
        }

        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                ExternalDocumentStatus.UnderConsideration => 0,
                ExternalDocumentStatus.Considered => 1,
                _ => 0,
            };
        }

        public static ExternalDocumentStatus BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => ExternalDocumentStatus.UnderConsideration,
                1 => ExternalDocumentStatus.Considered,
            };
        }
    }
}
