using AppDocumentManagment.DB.Controllers;
using AppDocumentManagment.DB.Models;
using AppDocumentManagment.UI.Utilities;
using AppDocumentManagment.UI.Views;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AppDocumentManagment.UI.ViewModels
{
    public class InitialAuthorizationViewModel : BaseViewModelClass
    {
        private InitialAuthorizationWindow InitialAuthorizationWindow { get; set; }
        public SolidColorBrush LoginBorderColor { get; set; } = new SolidColorBrush(Colors.DimGray);
        public int LoginBorderThickness { get; set; } = 1;
        public string LoginTipText { get; set; } = "Введите логин... Логин должен состоять из любых латинских букв(в верхнем или нижнем регистре), цифр и подчеркивания";
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                string verifiableLogin = ValidateData.TrimInputString(value);
                if (!ValidateData.ValidateString(verifiableLogin, 5))
                {
                    LoginBorderThickness = 2;
                    OnPropertyChanged(nameof(LoginBorderThickness));
                    LoginBorderColor = Brushes.Red;
                    OnPropertyChanged(nameof(LoginBorderColor));
                    LoginTipText = "Проверьте корректность введенного логина";
                    OnPropertyChanged(nameof(LoginTipText));
                }
                else
                {
                    LoginBorderColor = Brushes.DimGray;
                    OnPropertyChanged(nameof(LoginBorderColor));
                    LoginBorderThickness = 1;
                    OnPropertyChanged(nameof(LoginBorderThickness));
                    LoginTipText = "Логин введен";
                    OnPropertyChanged(nameof(LoginTipText));
                }
                login = verifiableLogin;
                OnPropertyChanged(nameof(Login));
            }
        }

        public SolidColorBrush PasswordBorderColor { get; set; } = new SolidColorBrush(Colors.DimGray);
        public int PasswordBorderThickness { get; set; } = 1;
        public string PasswordTipText { get; set; } = "Введите пароль...Пароль должен содержать символы латинского алфавита в верхнем и нижнем регистре, цифры. Недопустимо вводить спецсимволы (*,.<>/?!@&^%$#(){}[]).";

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                string verifiablePassword = ValidateData.TrimInputString(value);
                if (!ValidateData.ValidateString(verifiablePassword, 5))
                {
                    PasswordBorderThickness = 2;
                    OnPropertyChanged(nameof(PasswordBorderThickness));
                    PasswordBorderColor = Brushes.Red;
                    OnPropertyChanged(nameof(PasswordBorderColor));
                    PasswordTipText = "Проверьте корректность введенного пароля";
                    OnPropertyChanged(nameof(PasswordTipText));
                }
                else
                {
                    PasswordBorderColor = Brushes.DimGray;
                    OnPropertyChanged(nameof(PasswordBorderColor));
                    PasswordBorderThickness = 1;
                    OnPropertyChanged(nameof(PasswordBorderThickness));
                    PasswordTipText = "Пароль введен";
                    OnPropertyChanged(nameof(PasswordTipText));
                }
                password = verifiablePassword;
                OnPropertyChanged(nameof(Password));
            }
        }
        public ICommand IAuthorization => new RelayCommand(authorization => StartWorking());

        private void StartWorking()
        {

            if (string.IsNullOrEmpty(Login))
            {
                MessageBox.Show("Недопустим пустой логин");
                PasswordBorderThickness = 2;
                OnPropertyChanged(nameof(LoginBorderThickness));
                PasswordBorderColor = Brushes.Red;
                OnPropertyChanged(nameof(LoginBorderColor));
                PasswordTipText = "Проверьте корректность введенного логина";
                OnPropertyChanged(nameof(PasswordTipText));
                return;
            }
            string checkedLogin = ValidateData.TrimInputString(Login);
            if (!ValidateData.ValidateLogin(checkedLogin, 5))
            {
                MessageBox.Show("Некорректный логин. Логин должен состоять из любых латинских букв(в верхнем или нижнем регистре, цифр и подчеркивания");
                PasswordBorderThickness = 2;
                OnPropertyChanged(nameof(LoginBorderThickness));
                PasswordBorderColor = Brushes.Red;
                OnPropertyChanged(nameof(LoginBorderColor));
                PasswordTipText = "Проверьте корректность введенного логина";
                OnPropertyChanged(nameof(PasswordTipText));
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Недопустим пустой пароль!");
                PasswordBorderThickness = 2;
                OnPropertyChanged(nameof(PasswordBorderThickness));
                PasswordBorderColor = Brushes.Red;
                OnPropertyChanged(nameof(PasswordBorderColor));
                PasswordTipText = "Проверьте корректность введенного пароля";
                OnPropertyChanged(nameof(PasswordTipText));
                return;
            }
            string checkedPassword = ValidateData.TrimInputString(Password);
            if (!ValidateData.ValidatePassword(checkedPassword, 5))
            {
                MessageBox.Show("Некорректный пароль. Пароль должен содержать символы латинского алфавита в верхнем и нижнем регистре, цифры. Недопустимо вводить спецсимволы (*,.<>/?!@&^%$#(){}[]).");
                PasswordBorderThickness = 2;
                OnPropertyChanged(nameof(PasswordBorderThickness));
                PasswordBorderColor = Brushes.Red;
                OnPropertyChanged(nameof(PasswordBorderColor));
                PasswordTipText = "Проверьте корректность введенного пароля";
                OnPropertyChanged(nameof(PasswordTipText));
                return;
            }

            RegistredUserController controller = new RegistredUserController();
            RegistredUser currentUser = controller.GetRegistratedUser(checkedLogin, checkedPassword);
            if (currentUser.RegistredUserLogin == null)
            {
                MessageBox.Show($"Пользователь с логином {checkedLogin} не зарегистрирован\nОбратитесь к администратору приложения");
                Login = "";
                Password = "";
                return;
            }
            if (currentUser.IsRegistered == false)
            {
                MessageBox.Show($"Неверно введен пароль\nОбратитесь к администратору приложения");
                return;
            }
            MessageBox.Show($"Вход пользователя: {currentUser.RegistredUserLogin}");
            switch (currentUser.UserRole)
            {
                case UserRole.Administrator:
                    AdminPanelWindow adminPanel = new AdminPanelWindow();
                    adminPanel.Show();
                    InitialAuthorizationWindow.Close();
                    break;
                case UserRole.GeneralDirector: break;
                case UserRole.DeputyGeneralDirector: break;
                case UserRole.HeadOfDepartment: break;
                case UserRole.Performer: break;
            }
        }
    }
}
