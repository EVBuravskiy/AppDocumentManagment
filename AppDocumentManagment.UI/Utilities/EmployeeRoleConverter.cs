using AppDocumentManagment.DB.Models;

namespace AppDocumentManagment.UI.Utilities
{
    public class EmployeeRoleConverter
    {
        public static string ConvertToString(object value)
        {
            if (value is EmployeeRole role)
            {
                return role switch
                {
                    EmployeeRole.GeneralDirector => "Генеральный директор",
                    EmployeeRole.DeputyGeneralDirector => "Заместитель генерального директора",
                    EmployeeRole.HeadOfDepartment => "Начальник отдела",
                    EmployeeRole.Performer => "Исполнитель"
                };
            }
            return value.ToString();
        }

        public static EmployeeRole ConvertToEnum(string value)
        {
            return value switch
            {
                "Генеральный директор" => EmployeeRole.GeneralDirector,
                "Заместитель генерального директора" => EmployeeRole.DeputyGeneralDirector,
                "Начальник отдела" => EmployeeRole.HeadOfDepartment,
                "Исполнитель" => EmployeeRole.Performer
            };
        }
        public static int ToIntConvert(Enum value)
        {
            return value switch
            {
                EmployeeRole.GeneralDirector => 0,
                EmployeeRole.DeputyGeneralDirector => 1,
                EmployeeRole.HeadOfDepartment => 2,
                EmployeeRole.Performer => 3,
            };
        }

        public static EmployeeRole BackConvert(int value)
        {
            int inputvalue = (int)value;
            if (inputvalue == -1) inputvalue = 0;
            return inputvalue switch
            {
                0 => EmployeeRole.GeneralDirector,
                1 => EmployeeRole.DeputyGeneralDirector,
                2 => EmployeeRole.HeadOfDepartment,
                3 => EmployeeRole.Performer,
                _ => EmployeeRole.Performer,
            };
        }
    }
}
