using ecomZadanie.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ecomZadanie.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            RefreshData();
            FillVisibleUsers();
        }

        private bool _searchByFirstName = false;
        public bool SearchByFirstName
        {
            get { return _searchByFirstName; }
            set
            {
                SetProperty(ref _searchByFirstName, value);
            }
        }

        public ObservableCollection<User> AllUsers { get; } = new ObservableCollection<User>();
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

        public ObservableCollection<User> VisibleUsers { get; set; } = new ObservableCollection<User>();
        private void FillVisibleUsers()
        {
            foreach (var user in AllUsers)
            {
                VisibleUsers.Add(user);
            }
        }


        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ?? (_searchCommand = new Command<string>((text) =>
        {
            if (text != null)
            {

                VisibleUsers.Clear();
                text = text.ToLower();
                foreach (var user in AllUsers) //TODO find a better way to do that
                {
                    if (SearchByFirstName ?
                        user.FirstName.ToLower().StartsWith(text) :
                        user.LastName.ToLower().StartsWith(text))
                    {
                        VisibleUsers.Add(user);
                    }
                }
            }
            else { FillVisibleUsers(); }

            Debug.WriteLine(VisibleUsers);
        }));
    }
}
