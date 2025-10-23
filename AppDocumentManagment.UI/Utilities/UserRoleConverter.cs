using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.UI.Utilities
{
    public class UserRoleConverter
    {
        public static string ConvertToString(object value)
        {
            if (value is UserRole role)
            {
                return role switch
                {
                    UserRole.Administrator => "Администратор",
                    UserRole.GeneralDirector => "Генеральный директор",
                    UserRole.DeputyGeneralDirector => "Заместитель генерального директора",
                    UserRole.HeadOfDepartment => "Начальник отдела",
                    UserRole.Performer => "Специалист",
                    UserRole.Сlerk => "Делопроизводитель",
                };
            }
            return value.ToString();
        }

        public static UserRole ConvertToEnum(string value)
        {
            return value switch
            {
                "Администратор" => UserRole.Administrator,
                "Генеральный директор" => UserRole.GeneralDirector,
                "Заместитель генерального директора" => UserRole.DeputyGeneralDirector,
                "Начальник отдела" => UserRole.HeadOfDepartment,
                "Специалист" => UserRole.Performer,
                "Делопроизводитель" => UserRole.Сlerk,
            };
        }

        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                UserRole.Administrator => 0,
                UserRole.GeneralDirector => 1,
                UserRole.DeputyGeneralDirector => 2,
                UserRole.HeadOfDepartment => 3,
                UserRole.Performer => 4,
                UserRole.Сlerk => 5,
            };
        }

        public static UserRole BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 4;
            return inputvalue switch
            {
                0 => UserRole.Administrator,
                1 => UserRole.GeneralDirector,
                2 => UserRole.DeputyGeneralDirector,
                3 => UserRole.HeadOfDepartment,
                4 => UserRole.Performer,
                5 => UserRole.Сlerk,
            };
        }
    }
}

