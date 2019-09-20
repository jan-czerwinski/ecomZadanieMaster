using ecomZadanie.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;


namespace ecomZadanie.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;
        public ObservableCollection<User> AllUsers { get; } = new ObservableCollection<User>();
        public ObservableCollection<User> VisibleUsers { get; set; } = new ObservableCollection<User>();

        bool _searchByFirstName = false;
        public bool SearchByFirstName
        {
            get { return _searchByFirstName; }
            set
            {
                SetProperty(ref _searchByFirstName, value);
            }
        }
        string _searchText = String.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }
        public DelegateCommand<string> SearchCommand { get; private set; }
        public DelegateCommand TextChangeInSearchCommand { get; private set; }
        public DelegateCommand UserTappedCommand { get; private set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            _navigationService = navigationService;
            RefreshData();
            SearchCommand = new DelegateCommand<string>(Search);
            TextChangeInSearchCommand = new DelegateCommand(TextChangeInSearch);
            UserTappedCommand = new DelegateCommand(UserTapped);
            FillVisibleUsers();
        }

        private void UserTapped()
        {
            _navigationService.NavigateAsync("UserDetailsPage");
        }

        private void FillVisibleUsers()
        {
            VisibleUsers.Clear();
            foreach (var user in AllUsers)
            {
                VisibleUsers.Add(user);
            }
        }
        private void Search(string text)
        {
            VisibleUsers.Clear();
            text = text.ToLower();
            foreach (var user in AllUsers)
            {
                if (SearchByFirstName ?
                    user.FirstName.ToLower().StartsWith(text) :
                    user.LastName.ToLower().StartsWith(text))
                {
                    VisibleUsers.Add(user);
                }
            }
        }

        private void TextChangeInSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FillVisibleUsers();
            }
        }

        private UserRoot UsersRoot { get; set; }
        public async void RefreshData()
        {
            using (HttpClient _client = new HttpClient())
            {
                UsersRoot = new UserRoot();
                var response = await _client.GetStringAsync(Constants.UsersUrl);
                UsersRoot = JsonConvert.DeserializeObject<UserRoot>(response);
            }
            if (UsersRoot.IsSuccess)
            {
                foreach (var user in UsersRoot.Data)
                {
                    AllUsers.Add(user);
                }
            }
        }
    }
}
