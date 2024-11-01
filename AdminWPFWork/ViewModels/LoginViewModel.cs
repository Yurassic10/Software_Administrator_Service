//using AdminWPFWork.Command;
using AdminWPFWork.View;
using AdminWPFWork.Windows;
using BLL.IServices;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AdminWPFWork.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IServiceSuperAdmin _serviceSuperAdmin;
        public UserViewModel UserViewModel { get; set; } // Доступ до UserViewModel
        private string _emailEntered;
        private string _passwordEntered;
        private bool _actionAllowed;

        public LoginViewModel(IServiceSuperAdmin serviceSuperAdmin)
        {
            _serviceSuperAdmin = serviceSuperAdmin;
            LogginAdmin = new RelayCommand(_param => Login(), _param => CanLogin());
        }

        public ICommand LogginAdmin { get; set; }

        public string EmailEntered
        {
            get => _emailEntered;
            set
            {
                _emailEntered = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested(); // Оновлення доступності LogginAdmin
            }
        }

        public string PasswordEntered
        {
            get => _passwordEntered;
            set
            {
                _passwordEntered = value;
                OnPropertyChanged();
                CommandManager.InvalidateRequerySuggested(); // Оновлення доступності LogginAdmin
            }
        }

        public bool ActionAllowed
        {
            get => _actionAllowed;
            set
            {
                _actionAllowed = value;
                OnPropertyChanged();
                // Оновлення UserViewModel ActionAllowed після зміни
                //if (UserViewModel != null)
                //{
                //    UserViewModel.ActionAllowed = _actionAllowed;
                //}
            }
        }


        public bool IsLoggedIn { get; private set; }

        public void Login()
        {
            var user = _serviceSuperAdmin.GetByEmail(EmailEntered);
            //  user.Salt.ToString()
            var hashedPassword = HashPassword(PasswordEntered, user.Salt); 
            IsLoggedIn = _serviceSuperAdmin.IsLogin(EmailEntered, hashedPassword); 
            // Хешування введеного пароля разом із сіллю
            //var hashedPassword = HashPassword(PasswordEntered, user.Salt.ToString());


            if (IsLoggedIn)
            {
                
                MessageBox.Show("Ви успішно entered в систему!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                _actionAllowed = CheckUserRole(user.RoleId);
                // Відкриття UserView
                //UserView userView = new UserView();
                //userView.Show(); // або userView.ShowDialog() 
                //Application.Current.MainWindow.Close(); 

                // Відкриття основного вікна
                WindowMain mainWindow = new WindowMain();
                //Application.Current.MainWindow = mainWindow; 
                mainWindow.Show();

                // Закриття вікна входу логуван
                var currentLoginWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is LoginView);
                currentLoginWindow?.Close();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль. Спробуйте ще раз.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            //UserViewModel.ActionAllowed=ActionAllowed;
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        // Метод для хешування пароля з сіллю
        private byte[] HashPassword(string pass, Guid salt)
        {
            using (var algorithm = SHA512.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass + salt.ToString()));
            }
            //var algorithm = SHA512.Create();
            //return algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass + salt));
        }
        private bool CheckUserRole(int roleId)
        {
            return roleId == 1;  // || roleId == 2

        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(EmailEntered) 
                && !string.IsNullOrWhiteSpace(PasswordEntered);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

