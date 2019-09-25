using ecomZadanie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ecomZadanie.Data
{
    class RestService
    {
        public ObservableCollection<User> AllUsers { get; private set; }
        private UserRoot Root { get; set; }
        public async Task<ObservableCollection<User>> GetUsers()
        {
            try
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
                Debug.WriteLine("Connected to api, but IsSuccess value is false");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Can't get users: {0}", e);
            }
            return new ObservableCollection<User>();
        }

        private UserDetailsRoot DetailsRoot { get; set; }
        public async Task<UserDetails> GetUserDetails(int Id)
        {
            try
            {
                using (HttpClient _client = new HttpClient())
                {
                    DetailsRoot = new UserDetailsRoot();
                    string url = string.Format(Constants.UserDetailBaseUrl, Id);
                    var response = await _client.GetStringAsync(url);
                    DetailsRoot = JsonConvert.DeserializeObject<UserDetailsRoot>(response);
                }
                if (DetailsRoot.IsSuccess)
                {
                    return DetailsRoot.Data;
                }
                Debug.WriteLine("Connected to api, but IsSuccess value is false");
            }
            catch (Exception e)
            {
                Debug.WriteLine("Can't get user details: {0}", e);
            }
            return new UserDetails();
        }
    }
}
