using ecomZadanie.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace ecomZadanie.Data
{
    public interface IRestService
    {
        Task<ObservableCollection<User>> RefreshDataAsync();
        //Task<UserDetails> GetDetailsAsync(int Id);

    }
}
