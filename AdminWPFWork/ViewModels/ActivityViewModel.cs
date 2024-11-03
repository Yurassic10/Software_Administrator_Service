using BLL.IServices;
using DTO.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdminWPFWork.ViewModels
{
    public class ActivityViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private readonly IServiceActivity _serviceActivity;
        private readonly IServiceSuperAdmin _serviceSuperAdmin;
        private readonly LoginViewModel _loginViewModel;

        public ObservableCollection<Activity> Activities { get; }

        public ICommand SearchActivityCommand { get; }

        private int _searchId;
        private string _searchResult;
        private bool _actionAllowed;

        public ActivityViewModel(IServiceActivity serviceActivity,IServiceSuperAdmin serviceSuperAdmin,LoginViewModel loginViewModel)
        {
            _serviceActivity = serviceActivity;
            _loginViewModel = loginViewModel;
            _serviceSuperAdmin = serviceSuperAdmin;

            Activities = new ObservableCollection<Activity>();
            LoadActivities();

            SearchActivityCommand = new RelayCommand(_param => SearchActivity(), _param => ActionAllowed);


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
        private void LoadActivities()
        {
            var activities = _serviceActivity.GetProducts();
            Activities.Clear(); // для очищення попереднього
            foreach (var activity in activities)
            {
                if (!Activities.Any(u => u.Id == activity.Id)) // Перевірка на дублікат
                {
                    Activities.Add(activity);
                }
            }
        }
        private void SearchActivity()
        {
            var activityById = _serviceActivity.GetById(SearchId);

            if (activityById != null)
            {
                var admin = _serviceSuperAdmin.GetById(activityById.AdminId);
                var deletedUser = _serviceSuperAdmin.GetById(activityById.UserId);
                if (deletedUser != null)
                {
                    SearchResult = $"Користувач: {admin.FirstName} зробив дію над користувачем: {deletedUser.FirstName}";
                }
                else
                {
                    SearchResult = $"Користувач: {admin.FirstName} зробив дію.";
                }
            }
            else
            {
                SearchResult = "Таку дію не знайдено.";
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
