using ecomZadanie.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ecomZadanie.Data
{
    public class RestService : IRestService
    {
        private readonly HttpClient _client;
        public RestService()
        {
            _client = new HttpClient();
        }
        public ObservableCollection<User> Users { get; private set; }
        public async Task<ObservableCollection<User>> RefreshDataAsync()
        {
            Users = new ObservableCollection<User>();

            var uri = new Uri(string.Format(Constants.UsersUrl, string.Empty));
            try
            {
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Users = JsonConvert.DeserializeObject<ObservableCollection<User>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Users;

        }
    }
}
