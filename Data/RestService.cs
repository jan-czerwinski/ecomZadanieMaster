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
        private UserRoot UsersRoot { get; set; }
        public async Task<ObservableCollection<User>> RefreshData()
        {
            using (HttpClient _client = new HttpClient())
            {
                UsersRoot = new UserRoot();
                var response = await _client.GetStringAsync(Constants.UsersUrl);
                UsersRoot = JsonConvert.DeserializeObject<UserRoot>(response);
            }
            if (UsersRoot.IsSuccess)
            {
                return new ObservableCollection<User>(UsersRoot.Data);
            }
            return null;
        }
    }
}
