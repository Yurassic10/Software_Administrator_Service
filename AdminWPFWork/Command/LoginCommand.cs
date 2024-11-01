//using AdminWPFWork.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Input;

//namespace AdminWPFWork.Command
//{
//    public class LoginCommand : ICommand
//    {
//        private readonly LoginViewModel _loginViewModel;
//        public event EventHandler? CanExecuteChanged;

//        public LoginCommand(LoginViewModel loginViewModel)
//        {
//            _loginViewModel = loginViewModel;
//        }
        

//        public bool CanExecute(object? parameter)
//        {
//            // Наприклад, активуйте кнопку, якщо Email і Password не порожні
//            return !string.IsNullOrWhiteSpace(_loginViewModel.EmailEntered) &&
//                   !string.IsNullOrWhiteSpace(_loginViewModel.PasswordEntered);
//            //return true;
//        }

//        public void Execute(object? parameter)
//        {
//            _loginViewModel.Login();
//            if (_loginViewModel.IsLoggedIn)
//            {
//                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
//            }
//            else
//            {
//                MessageBox.Show("Login failed. Check your credentials.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//            CanExecuteChanged?.Invoke(this, new EventArgs()); // Додав але працює і без цього
//            //// Виконує спробу входу
//            //_loginViewModel.Login();

//            //// Викликає подію CanExecuteChanged для оновлення доступності команди
//            //CanExecuteChanged?.Invoke(this, EventArgs.Empty);
//        }
//        public void RaiseCanExecuteChanged()
//        {
//            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
//        }
//    }
//}
