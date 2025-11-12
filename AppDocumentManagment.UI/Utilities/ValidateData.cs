using System.Text.RegularExpressions;

namespace AppDocumentManagment.UI.Utilities
{
    public class ValidateData
    {
        //string phoneNumber = "123-456-7890";
        //string pattern = @"^\d{3}-\d{3}-\d{4}$"; // Шаблон для номера телефона в формате XXX-XXX-XXXX


        static public bool ValidateString(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            if (inputString.Length < length) return false;
            return true;
        }

        static public string TrimInputString(string inputString)
        {
            return inputString.Trim();
        }

        static public bool ValidatePassword(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            if (inputString.Length < length) return false;
            string pattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[a-zA-Z0-9]{5,}$";
            return Regex.IsMatch(inputString, pattern);
        }

        static public bool ValidateLogin(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            if (inputString.Length < length) return false;
            string pattern = @"^[a-zA-Z][a-zA-Z0-9_]{5,25}$";
            return Regex.IsMatch(inputString, pattern);
        }

        static public bool ValidateEmail(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            if (inputString.Length < length) return false;
            string pattern = @"^[-\w.]+@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,4}$";
            return Regex.IsMatch(inputString, pattern);
        }

        static public bool ValidatePhone(string inputString, int length)
        {
            if (String.IsNullOrEmpty(inputString)) return false;
            string pattern = @"^\d{11}$";
            return Regex.IsMatch(inputString, pattern);
        }
    }
}

