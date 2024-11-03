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

        private int _userRoleId;
        private int _loggedInId;

        public LoginViewModel(IServiceSuperAdmin serviceSuperAdmin)
        {
            _serviceSuperAdmin = serviceSuperAdmin;
            LogginAdmin = new RelayCommand(_param => Login(), _param => CanLogin());

        }

        public ICommand LogginAdmin { get; set; }


        #region Property
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

        public int UserRoleId
        {
            get => _userRoleId;
            private set
            {
                _userRoleId = value;
                OnPropertyChanged();
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
            
            }
        }
        public int LoggedInId
        {
            get => _loggedInId;
            set
            {
                _loggedInId = value;
                OnPropertyChanged();
            }
        }


        public bool IsLoggedIn { get; private set; }
        #endregion

        #region Methods
        public void Login()
        {
            var user = _serviceSuperAdmin.GetByEmail(EmailEntered);
            var hashedPassword = HashPassword(PasswordEntered, user.Salt); 
            IsLoggedIn = _serviceSuperAdmin.IsLogin(EmailEntered, hashedPassword); 


            if (IsLoggedIn)
            {
                UserRoleId = user.RoleId;
                MessageBox.Show("Ви успішно entered в систему!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
                _actionAllowed = CheckUserRole(user.RoleId);
                LoggedInId = user.Id;

                OnPropertyChanged(nameof(ActionAllowed));

                WindowMain mainWindow = new WindowMain(this); 
                mainWindow.Show();


                var currentLoginWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is LoginView);
                currentLoginWindow?.Close();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль. Спробуйте ще раз.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        private byte[] HashPassword(string pass, Guid salt)
        {
            using (var algorithm = SHA512.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass + salt.ToString()));
            }
        }
        private bool CheckUserRole(int roleId)
        {

            return roleId == 1 || roleId == 2;  

        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(EmailEntered) 
                && !string.IsNullOrWhiteSpace(PasswordEntered);
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

