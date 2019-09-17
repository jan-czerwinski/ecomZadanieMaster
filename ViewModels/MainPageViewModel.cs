using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
