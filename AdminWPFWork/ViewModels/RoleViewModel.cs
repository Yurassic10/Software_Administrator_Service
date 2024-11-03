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
    public class RoleViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IServiceRole _serviceRole;
        private readonly LoginViewModel _loginViewModel;
        public ObservableCollection<Role> Roles { get; } // Колекція для списку ролей
        public ICommand SearchRoleCommand { get; }
        
        private int _searchId;
        private string _searchResult;
        private bool _actionAllowed;



        public RoleViewModel(IServiceRole serviceRole, LoginViewModel loginViewModel)
        {
            _serviceRole = serviceRole;
            Roles = new ObservableCollection<Role>();
            LoadRoles();

            SearchRoleCommand = new RelayCommand(_param => SearchRole(), _param => ActionAllowed);
            _loginViewModel = loginViewModel;


            _loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;

            ActionAllowed = _loginViewModel.ActionAllowed;
        }
        private void LoginViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.ActionAllowed))
            {
                ActionAllowed = _loginViewModel.ActionAllowed;
            }
        }
        #region Property
        public int SearchId
        {
            get => _searchId;
            set
            {
                _searchId = value;
                OnPropertyChanged();
            }
        }
        public string SearchResult
        {
            get => _searchResult;
            set
            {
                _searchResult = value;
                OnPropertyChanged();
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
        private void SearchRole()
        {
            var roleById = _serviceRole.GetById(SearchId);
            //var userByEmail = _serviceSuperAdmin.GetByEmail(SearchEmail);
            //Users.Clear();
            //Додав
            if (roleById != null)
            {
                SearchResult = $"Користувач з роллю: {roleById.Name}";
                //Users.Clear();
                //Users.Add(userById);
            }
            else
            {
                SearchResult = "Користувача не знайдено.";
            }
        }

        private void LoadRoles()
        {
            var roles = _serviceRole.GetProducts();
            Roles.Clear(); // для очищення попереднього
            foreach (var role in roles)
            {
                if (!Roles.Any(u => u.Id == role.Id)) // Перевірка на дублікат
                {
                    Roles.Add(role);
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
