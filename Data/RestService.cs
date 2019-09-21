using ecomZadanie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ecomZadanie.Data
{
    class RestService
    {
        public ObservableCollection<User> AllUsers { get; private set; }
        private UserRoot Root { get; set; }
        public async Task<ObservableCollection<User>> RefreshData()
        {
            using (HttpClient _client = new HttpClient())
            {
                Root = new UserRoot();
                var response = await _client.GetStringAsync(Constants.UsersUrl);
                Root = JsonConvert.DeserializeObject<UserRoot>(response);
            }
            if (Root.IsSuccess)
            {
                return new ObservableCollection<User>(Root.Data);
            }
            return null;
        }

        private UserDetailsRoot DetailsRoot { get; set; }
        public async Task<UserDetails> GetUserDetails(int Id)
        {
            using (HttpClient _client = new HttpClient())
            {
                DetailsRoot = new UserDetailsRoot();
                var response = await _client.GetStringAsync(Constants.UserDetailUrl(Id));
                DetailsRoot = JsonConvert.DeserializeObject<UserDetailsRoot>(response);
            }
            if (DetailsRoot.IsSuccess)
            {
                return DetailsRoot.Data;
            }
            return null;
        }
    }
}
