using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AdminWPFWork.View;
using AdminWPFWork.ViewModels;
using BLL.IServices;
using BLL.Services;
using DTO.Model;

namespace AdminWPFWork.Windows
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        private readonly LoginViewModel _loginViewModel;
        public ObservableCollection<User> Users { get; } 
        public WindowMain(LoginViewModel loginViewModel) 
        {
            InitializeComponent();


            IServiceSuperAdmin serviceSuperAdmin = new ServiceSuperAdmin(); 
            IServiceActivity serviceActivity = new ServiceActivity(); 
            IServiceRole serviceRole = new ServiceRole();
            IServiceStatus serviceStatus = new ServiceStatus();
            _loginViewModel = loginViewModel; 
            

            WindowMainViewModel viewModel = new WindowMainViewModel(serviceSuperAdmin, serviceActivity,serviceRole,serviceStatus, _loginViewModel);
            this.DataContext = viewModel; 

        }
    }
}
