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
        private UserDetailsPageVisibility _isVisible;
        public UserDetailsPageVisibility IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }
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
            IsVisible = new UserDetailsPageVisibility()
            {
                ActivityIndicator = true,
                UserData = false,
                SomethingWentWrong = false
            };
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            int.TryParse(parameters.GetValue<string>("Id"), out int Id);
            FillData(Id);
        }
        private async void FillData(int Id)
        {
            var result = await restService.GetUserDetails(Id);
            UserData = result;
            IsVisible = new UserDetailsPageVisibility()
            {
                ActivityIndicator = false,
                UserData = UserData.Id != 0,
                SomethingWentWrong = UserData.Id == 0
            };
        }
    }
}
