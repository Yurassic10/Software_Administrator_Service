using AdminWPFWork.ViewModels;
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
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView()
        {
            InitializeComponent();
            DataContext = new UserViewModel(new ServiceSuperAdmin(),new ServiceActivity(),new LoginViewModel(new ServiceSuperAdmin()));
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is UserViewModel viewModel)
            {
                var passwordBox = sender as PasswordBox;
                viewModel.NewPassword = Encoding.UTF8.GetBytes(passwordBox.Password);
            }
        }

    }
}
