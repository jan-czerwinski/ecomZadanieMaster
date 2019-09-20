using ecomZadanie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

namespace ecomZadanie.Data
{
    class RestService
    {
        private ObservableCollection<User> AllUsers { get; } = new ObservableCollection<User>();
        private UserRoot UsersRoot { get; set; }
        private async void RefreshData()
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

        public ObservableCollection<User> GetAllUsers()
        {
            RefreshData();
            return AllUsers;
        }
    }
}
