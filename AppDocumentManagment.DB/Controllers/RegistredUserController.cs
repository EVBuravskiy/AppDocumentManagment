using AppDocumentManagment.DB.Models;
using AppDocumentManagment.DB.Utilities;

namespace AppDocumentManagment.DB.Controllers
{
    public class RegistredUserController
    {
        public bool AddRegistratedUser(RegistredUser inputUser)
        {
            bool result = false;
            if (inputUser != null)
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    context.RegistredUsers.Add(inputUser);
                    context.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        public bool UpdateRegistratedUser(RegistredUser inputUser)
        {
            bool result = false;
            if (inputUser != null)
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    RegistredUser currentUser = context.RegistredUsers.Where(x => x.RegistredUserID == inputUser.RegistredUserID).FirstOrDefault();
                    if (currentUser == null)
                    {
                        result = AddRegistratedUser(inputUser);
                        return result;
                    }
                    currentUser.RegistredUserLogin = inputUser.RegistredUserLogin;
                    currentUser.RegistredUserPassword = inputUser.RegistredUserPassword;
                    currentUser.UserRole = inputUser.UserRole;
                    currentUser.Employee = inputUser.Employee;
                    currentUser.IsRegistered = true;
                    currentUser.RegistredUserTime = inputUser.RegistredUserTime;
                    context.RegistredUsers.Update(currentUser);
                    context.SaveChanges();
                    result = true;
                }
            }
            return true;
        }

        public bool RemoveRegistratedUser(RegistredUser inputUser)
        {
            bool result = false;
            if (inputUser != null)
            {
                using (ApplicationContext context = new ApplicationContext())
                {
                    RegistredUser currentUser = context.RegistredUsers.Where(x => x.RegistredUserID == inputUser.RegistredUserID).FirstOrDefault();
                    if (currentUser == null) return result;
                    context.RegistredUsers.Remove(currentUser);
                    context.SaveChanges();
                    result = true;
                }
            }
            return result;
        }

        public RegistredUser GetRegistratedUser(string login, string password)
        {
            RegistredUser registredUser = UserAutentication(login, password);
            if (registredUser == null)
            {
                return null;
            }
            return registredUser;
        }

        private RegistredUser UserAutentication(string login, string password)
        {
            RegistredUser checkedRegistredUser = new RegistredUser();
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)) return checkedRegistredUser;
            using (ApplicationContext context = new ApplicationContext())
            {
                RegistredUser registredUser = context.RegistredUsers.Where(x => x.RegistredUserLogin == login).FirstOrDefault();
                if (registredUser == null)
                {
                    checkedRegistredUser.UserRole = UserRole.Performer;
                    return checkedRegistredUser;
                }
                else
                {
                    checkedRegistredUser.RegistredUserLogin = login;
                    string forGettingHash = $"{login}-{password}";
                    string passHash = PassHasher.CalculateMD5Hash(forGettingHash);
                    if (String.Equals(passHash, registredUser.RegistredUserPassword))
                    {
                        checkedRegistredUser.UserRole = registredUser.UserRole;
                        checkedRegistredUser.RegistredUserTime = registredUser.RegistredUserTime;
                        checkedRegistredUser.Employee = registredUser.Employee;
                        checkedRegistredUser.EmployeeID = registredUser.EmployeeID;
                        checkedRegistredUser.IsRegistered = registredUser.IsRegistered;
                        return checkedRegistredUser;
                    }
                }
                return checkedRegistredUser;
            }
        }

        public List<RegistredUser> GetAllRegistredUsers()
        {
            List<RegistredUser> registredUsers = new List<RegistredUser>();
            using (ApplicationContext context = new ApplicationContext())
            {
                registredUsers = context.RegistredUsers.ToList();
            }
            return registredUsers;
        }

        public RegistredUser GetRegistredUser(int employeeID)
        {
            List<RegistredUser> registredUsers = new List<RegistredUser>();
            using (ApplicationContext context = new ApplicationContext())
            {
                registredUsers = context.RegistredUsers.Where(x => x.EmployeeID == employeeID).ToList();
            }
            if (registredUsers.Count > 0)
            {
                return registredUsers[0];
            }
            return null;
        }
    }
}
