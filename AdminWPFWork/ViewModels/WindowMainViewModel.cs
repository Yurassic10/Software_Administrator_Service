using BLL.IServices;
using BLL.Services;
using DataAccessLogic.ADO;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminWPFWork.ViewModels
{
    public class WindowMainViewModel : INotifyPropertyChanged
    {
        private BaseViewModel _currentViewModel;
        private readonly IServiceSuperAdmin _serviceSuperAdmin;
        private readonly IServiceActivity _serviceActivity;
        private readonly IServiceRole _serviceRole;


        private readonly LoginViewModel _loginViewModel;
        private UserViewModel _userViewModel;
        private RoleViewModel _roleViewModel;
        private ActivityViewModel _activityViewModel;


        private bool _actionAllowed;

        
        public ICommand ShowUserViewModelCommand { get; }
        public ICommand ShowViewRoleModelCommand { get; }
        public ICommand ShowViewActivityModelCommand { get; }
        public ObservableCollection<User> Users { get; } // Колекція для списку користувачів

        public WindowMainViewModel(IServiceSuperAdmin serviceSuperAdmin, IServiceActivity serviceActivity,IServiceRole serviceRole, LoginViewModel loginViewModel)
        {
            _serviceSuperAdmin = serviceSuperAdmin;
            _serviceActivity = serviceActivity;
            _loginViewModel = loginViewModel;
            _serviceRole = serviceRole;
            Users = new ObservableCollection<User>();


            ShowUserViewModelCommand = new RelayCommand(o => ShowUserViewModel());
            ShowViewRoleModelCommand = new RelayCommand(o => ShowRoleViewModel());
            ShowViewActivityModelCommand = new RelayCommand(o => ShowActivityViewModel());


            _userViewModel = new UserViewModel(_serviceSuperAdmin, _serviceActivity, _loginViewModel);
            _roleViewModel = new RoleViewModel(_serviceRole,_loginViewModel); // Створюємо один раз
            _activityViewModel = new ActivityViewModel(_serviceActivity,_serviceSuperAdmin,_loginViewModel);

           
            CurrentViewModel = _userViewModel;


            _loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;
            LoadUsers();
        }
        private void LoginViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.ActionAllowed))
            {
                ActionAllowed = _loginViewModel.ActionAllowed;
            }
        }

        #region Property
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        public bool ActionAllowed
        {
            get => _actionAllowed;
            set
            {
                _actionAllowed = value;
                OnPropertyChanged(nameof(ActionAllowed));
            }
        }
        #endregion

        #region Methods
        private void LoadUsers()
        {
            var users = _serviceSuperAdmin.GetProducts();
            Users.Clear(); // для очищення попереднього
            foreach (var user in users)
            {
                if (!Users.Any(u => u.Id == user.Id)) // Перевірка на дублікат
                {
                    Users.Add(user);
                }
            }
        }

        private void ShowUserViewModel()
        {
            CurrentViewModel = _userViewModel;
        }

        private void ShowRoleViewModel()
        {
            CurrentViewModel = _roleViewModel;
        }
        private void ShowActivityViewModel()
        {
            CurrentViewModel = _activityViewModel;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
