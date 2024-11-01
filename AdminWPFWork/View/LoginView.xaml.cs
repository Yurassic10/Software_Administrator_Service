using AdminWPFWork.ViewModels;
using BLL.IServices;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdminWPFWork.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        //public LoginView(IServiceSuperAdmin serviceSuperAdmin)
        //{
        //    InitializeComponent();
        //    DataContext = new LoginViewModel(serviceSuperAdmin);
        //}
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(new ServiceSuperAdmin());
        }
        //public LoginView(LoginViewModel viewModel)
        //{
        //    InitializeComponent();
        //    DataContext = viewModel;
        //}
        public void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //var passwordBox = sender as PasswordBox;
            //if (passwordBox != null)
            //{
            //    // Отримуємо DataContext, щоб отримати доступ до LoginViewModel
            //    var viewModel = this.DataContext as LoginViewModel;
            //    if (viewModel != null)
            //    {
            //        viewModel.PasswordEntered = passwordBox.Password; // Встановлюємо введений пароль
            //    }
            //}

            var passwordBox = sender as PasswordBox;
            var viewModel = DataContext as LoginViewModel;

            viewModel.PasswordEntered = passwordBox.Password; // this.PasswordBox.Password
        }

    }
}
