using BLL.IServices;
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
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly IServiceSuperAdmin _serviceSuperAdmin;
        private readonly IServiceActivity _serviceActivity;
        private readonly LoginViewModel _loginViewModel;
        private int _searchId;
        private string _serchEmail;
        private User _selectedUser;
        private string _searchResult;
        private string _newFirstName;
        private string _newLastName;
        private string _newEmail;
        private byte[] _newPassword; 
        private int _newRoleId;
        private int _newStatusId;
        private bool _actionAllowed;

        public UserViewModel(IServiceSuperAdmin serviceSuperAdmin,IServiceActivity serviceActivity, LoginViewModel loginViewModel)
        {
            _serviceSuperAdmin = serviceSuperAdmin;
            _serviceActivity = serviceActivity;
            _loginViewModel = loginViewModel;
            Users = new ObservableCollection<User>();
            LoadUsers();
            DeleteUserCommand = new RelayCommand(_param => DeleteUser(), _param => CanDeleteUser()); //CanDeleteUser()
            SearchUserCommand = new RelayCommand(_param => SearchUser(),_param => CanSearchUser()); //_loginViewModel.ActionAllowed
            AddUserCommand = new RelayCommand(_param => AddUser());

            SearchEmail = _loginViewModel.EmailEntered; 
            InitializeActionAllowed();
            //ActionAllowed = _loginViewModel.ActionAllowed;

        }
        private void InitializeActionAllowed()
        {
            // Додаємо перевірку на null
            if (string.IsNullOrEmpty(_loginViewModel?.EmailEntered))
            {
                MessageBox.Show("Електронну пошту не передано. Перевірте вхід у систему.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                ActionAllowed = false;
                return;
            }

            var loggedInUser = _serviceSuperAdmin.GetByEmail(_loginViewModel.EmailEntered);

            if (loggedInUser != null)
            {
                ActionAllowed = CheckUserRole(loggedInUser.RoleId);
            }
            else
            {
                ActionAllowed = false;
                MessageBox.Show("Користувача не знайдено. Перевірте правильність введення електронної пошти.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
            if(SelectedUser!=null)
            {
                var activity = new Activity
                {
                    AdminId = 1,
                    UserId = SelectedUser.Id,
                    ActionId = 1,
                    CreatedAt = DateTime.Now
                };

                _serviceActivity.Add(activity);
                _serviceSuperAdmin.Delete(SelectedUser.Id);
                Users.Remove(SelectedUser);
                // Додав
                MessageBox.Show("Користувача успішно видалено.", "Видалення користувача", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadUsers();
            }
        }
        private bool CanDeleteUser()
        {
            //return ActionAllowed;
            return true;
        }
        private bool CanSearchUser()
        {

            //return _loginViewModel.ActionAllowed;
            return true;
            //return ActionAllowed;
        }
        private void SearchUser()
        {

            var userById = _serviceSuperAdmin.GetById(SearchId);
            var userByEmail = _serviceSuperAdmin.GetByEmail(SearchEmail);
            //Users.Clear();
            //Додав
            if (userById != null)
            {
                SearchResult = $"Користувач: {userById.FirstName} {userById.LastName}";
                //Users.Clear();
                //Users.Add(userById);
            }
            else
            {
                SearchResult = "Користувача не знайдено.";
            }
            //SearchResult = userById.FirstName;
        }
        private void AddUser()
        {
            // Генерація солі
            var salt = Guid.NewGuid(); //.ToString()
            var hashedPassword = Hash(NewPassword.ToString(), salt.ToString()); //salt.ToString()
            var newUser = new User
            {
                FirstName=NewFirstName, 
                LastName=NewLastName,
                Email=NewEmail,
                Password=hashedPassword,
                RoleId=NewRoleId,
                StatusId=NewStatusId,
                CreatedAt=DateTime.Now,
                UpdatedAt=DateTime.Now,
                Salt = salt // Guid.NewGuid()
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

        public ObservableCollection<User> Users { get; } // Колекція для списку користувачів

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
        private bool CheckUserRole(int roleId)
        {
            return roleId == 1 || roleId == 2; // 
        }

        public ICommand DeleteUserCommand { get; }
        public ICommand SearchUserCommand { get; }
        public ICommand AddUserCommand {  get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
