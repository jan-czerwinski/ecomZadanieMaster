using ecomZadanie.Data;
using ecomZadanie.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ecomZadanie.ViewModels
{
    public class UserDetailsPageViewModel : ViewModelBase
    {

        readonly RestService restService;

        private UserDetails _userData;
        public UserDetails UserData
        {
            get { return _userData; }
            set { SetProperty(ref _userData, value); }
        }
        public UserDetailsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            restService = new RestService();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            int.TryParse(parameters.GetValue<string>("Id"), out int Id);
            FillData(Id);
            Debug.WriteLine(Id);
        }

        private async void FillData(int Id)
        {
            var result = await restService.GetUserDetails(Id);
            UserData = result;
        }
    }
}
