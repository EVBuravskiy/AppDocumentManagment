using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.UI.Utilities
{
    public class DocumentTypeConverter
    {
        public static string ConvertToString(object value)
        {
            if (value is DocumentType type)
            {
                return type switch
                {
                    DocumentType.Contract => "Договор/контракт",
                    DocumentType.CommercialOffer => "Коммерческое предложение",
                    DocumentType.Letter => "Письмо/Сопроводительное письмо",
                    DocumentType.GovernmentLetter => "Представление/Требование",
                };
            }
            return value.ToString();
        }

        public static DocumentType ConvertToEnum(string value)
        {
            return value switch
            {
                "Договор/контракт" => DocumentType.Contract,
                "Коммерческое предложение" => DocumentType.CommercialOffer,
                "Письмо/Сопроводительное письмо" => DocumentType.Letter,
                "Представление/Требование" => DocumentType.GovernmentLetter,
            };
        }

        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                DocumentType.Contract => 0,
                DocumentType.CommercialOffer => 1,
                DocumentType.Letter => 2,
                DocumentType.GovernmentLetter => 3,
                _ => 0,
            };
        }

        public static DocumentType BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => DocumentType.Contract,
                1 => DocumentType.CommercialOffer,
                2 => DocumentType.Letter,
                3 => DocumentType.GovernmentLetter,
                _ => DocumentType.Contract
            };
        }
    }
}
