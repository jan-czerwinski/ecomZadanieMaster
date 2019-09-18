using ecomZadanie.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ecomZadanie.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            //Just a Users filler
            for (int i = 0; i < 20; i++)
            {
                User user = new User
                {
                    FirstName = "Alberto",
                    LastName = "Balsalm",
                    Id = i
                };
                Users.Add(user);
            }
        }


        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>((text) =>
                {
                    System.Diagnostics.Debug.WriteLine(text);
                }));
            }
        }
    }
}
