﻿using BLL.IServices;
using DataAccessLogic.ADO;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AdminWPFWork.ViewModels;
using AdminWPFWork.View;

namespace AdminWPFWork.ViewModels
{
    public class UserViewModel : BaseViewModel ,IDataErrorInfo
    {
        private readonly IServiceSuperAdmin _serviceSuperAdmin;
        private readonly IServiceActivity _serviceActivity;
        private readonly IServiceRole _serviceRole;
        private readonly IServiceStatus _serviceStatus;
        private readonly LoginViewModel _loginViewModel;

        private int _searchId;
        private string _serchEmail;
        private User _selectedUser;
        private string _searchResult;
        private string _newFirstName = string.Empty;
        private string _newLastName = string.Empty;
        private string _newEmail = string.Empty;
        private byte[] _newPassword; 
        private int _newRoleId;
        private int _newStatusId;
        private bool _actionAllowed;
        private string _isVisible;
        private int _IdForActivity;
        private int _newUserId;

        private string _errorMessage;

        public ObservableCollection<User> Users { get; } // Колекція для списку користувачів
        private ObservableCollection<Role> _roles;
        public ObservableCollection<Role> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Status> _status;
        public ObservableCollection<Status> Statuses
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public UserViewModel(IServiceSuperAdmin serviceSuperAdmin,IServiceActivity serviceActivity,IServiceRole serviceRole,IServiceStatus serviceStatus, LoginViewModel loginViewModel)
        {
            _serviceSuperAdmin = serviceSuperAdmin;
            _serviceActivity = serviceActivity;
            _loginViewModel = loginViewModel;
            _serviceRole = serviceRole;
            _serviceStatus = serviceStatus;
            Users = new ObservableCollection<User>();
            Roles = new ObservableCollection<Role>(_serviceRole.GetProducts());
            Statuses = new ObservableCollection<Status>(_serviceStatus.GetProducts());
            LoadUsers();
            ActionAllowed = _loginViewModel.ActionAllowed;
            IsVisible = Test();
            DeleteUserCommand = new RelayCommand(_param => DeleteUser(), _param => ActionAllowed); 
            SearchUserCommand = new RelayCommand(_param => SearchUser(),_param => ActionAllowed); 
            AddUserCommand = new RelayCommand(_param => AddUser());

            SearchEmail = _loginViewModel.EmailEntered; 
     
            

            _loginViewModel.PropertyChanged += LoginViewModel_PropertyChanged;
        }
        private void LoginViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.ActionAllowed))
            {
                _actionAllowed = _loginViewModel.ActionAllowed;
            }
        }

        public ICommand DeleteUserCommand { get; }
        public ICommand SearchUserCommand { get; }
        public ICommand AddUserCommand { get; }

        #region Methods

        private string Test()
        {
            if (ActionAllowed == false)
            {
                return "Hidden";
            }
            return "Visible";
        }
        public  bool CheckUserRoleUser(bool role)
        {
            _actionAllowed = role;
            return role;
        }

    
       
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

        private void DeleteUser()
        {
            if (SelectedUser != null)
            {
                var activity = new Activity
                {
                    AdminId = _loginViewModel.LoggedInId,
                    UserId = SelectedUser.Id,
                    ActionId = 1,
                    CreatedAt = DateTime.Now
                };
                IdForActivity = SelectedUser.Id;
                _serviceActivity.Add(activity);
                _serviceSuperAdmin.Delete(SelectedUser.Id);
                Users.Remove(SelectedUser);
                MessageBox.Show("Користувача успішно видалено.", "Видалення користувача", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadUsers();
            }
        }
       
        private void SearchUser()
        {

            var userById = _serviceSuperAdmin.GetById(SearchId);
            if (userById != null)
            {
                SearchResult = $"Користувач: {userById.FirstName} {userById.LastName}";
            }
            else
            {
                SearchResult = "Користувача не знайдено.";
            }
        }
        private void AddUser()
        {
            if (Users.Count == 0)
            {
                NewUserId = 1;
            }
            else
            {
                int maxId = Users.Max(x => x.Id);
                NewUserId = maxId + 1;
            }
            var salt = Guid.NewGuid(); 
            var hashedPassword = Hash(NewPassword.ToString(), salt.ToString()); 
            var newUser = new User
            {
                Id = NewUserId,
                FirstName =NewFirstName, 
                LastName=NewLastName,
                Email=NewEmail,
                Password=hashedPassword,
                RoleId=NewRoleId,
                StatusId=NewStatusId,
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
                Salt = salt 
            };
            _serviceSuperAdmin.Add(newUser);
            Users.Add(newUser);
            ClearNewUserFields(); 
        }
        public void ClearNewUserFields()
        {
            NewFirstName = NewLastName = NewEmail = string.Empty;
            NewRoleId = NewStatusId = 0;
            _newPassword = null;
        }
        
        private byte[] Hash(string pass, string salt)
        {
            using (var algorithm = SHA512.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass + salt));
            }
        }
        #endregion

        #region Property
        public int NewUserId
        {
            get => _newUserId;
            set
            {
                _newUserId = value;
                OnPropertyChanged();
            }
        }
        public int IdForActivity
        {
            get => _IdForActivity;
            set
            {
                _IdForActivity = value;
                OnPropertyChanged();
            }
        }
        public string IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                
                OnPropertyChanged();
            }
        }
        public int SearchId
        {
            get => _searchId;
            set 
            { 
                _searchId = value;
                OnPropertyChanged();
            }
        }

        public string SearchEmail
        {
            get
            {
                return _serchEmail;
            }
            set
            {
                _serchEmail = value;
                OnPropertyChanged();
            }
        }
        public User SelectedUser
        {
            get=> _selectedUser;
            set
            {
                _selectedUser = value;
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
        public string NewFirstName
        {
            get => _newFirstName;
            set
            {
                _newFirstName = value;
                OnPropertyChanged();
            }
        }
        public string NewLastName
        {
            get => _newLastName;
            set
            {
                _newLastName = value;
                OnPropertyChanged();
            }
        }
        public string NewEmail
        {
            get => _newEmail;
            set
            {
                _newEmail = value;
                OnPropertyChanged();
            }
        }
        public byte[] NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }
        public int NewRoleId
        {
            get => _newRoleId;
            set
            {
                _newRoleId = value;
                OnPropertyChanged();
            }
        }
        public int NewStatusId
        {
            get => _newStatusId;
            set
            {
                _newStatusId = value;
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
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;
                if (columnName == nameof(NewFirstName))
                {
                    if (NewFirstName.Length < 5 || NewFirstName.Length > 12)
                    {
                        result = "New first name should be between range 5-12";
                    }
                }
                else if (columnName == nameof(NewLastName))
                {
                    if (NewLastName.Length < 5 || NewLastName.Length > 12)
                    {
                        result = "New last name should be between range 5-12";
                    }
                }
                else if(columnName == nameof(NewEmail))
                {
                    if(!NewEmail.EndsWith("@gmail.com"))
                    {
                        result = "New email should be end with '@gmail.com' ";
                    }
                }
                ErrorMessage = result;
                return result;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
