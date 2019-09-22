﻿using ecomZadanie.Data;
using ecomZadanie.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ecomZadanie.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        readonly INavigationService _navigationService;
        public ObservableCollection<User> AllUsers { get; } = new ObservableCollection<User>();
        public ObservableCollection<User> VisibleUsers { get; set; } = new ObservableCollection<User>();
        readonly RestService restService;

        private MainPageVisibility _isVisible;
        public MainPageVisibility IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        private bool _searchByFirstName = false;
        public bool SearchByFirstName
        {
            get { return _searchByFirstName; }
            set { SetProperty(ref _searchByFirstName, value); }
        }

        string _searchText = String.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }
        public DelegateCommand<string> SearchCommand { get; private set; }
        public DelegateCommand TextChangeInSearchCommand { get; private set; }
        public DelegateCommand<User> UserTappedCommand { get; private set; }
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Strona główna";
            _navigationService = navigationService;
            restService = new RestService();
            SearchCommand = new DelegateCommand<string>(Search);
            TextChangeInSearchCommand = new DelegateCommand(TextChangeInSearch);
            UserTappedCommand = new DelegateCommand<User>(UserTapped);
            IsVisible = new MainPageVisibility() { ActivityIndicator = true, ListView = true, Label = false };
            FillAllUsers();
        }

        private void UserTapped(User user)
        {
            var navigationParams = new NavigationParameters
            {
                { "Id", user.Id }
            };
            _navigationService.NavigateAsync("UserDetailsPage", navigationParams);
        }
        private async void FillAllUsers()
        {
            var result = await restService.RefreshData();
            foreach (var user in result)
            {
                AllUsers.Add(user);
            }
            FillVisibleUsers();
            SetVisibility();
        }
        private void FillVisibleUsers()
        {
            VisibleUsers.Clear();
            foreach (var user in AllUsers)
            {
                VisibleUsers.Add(user);
            }

        }
        private void Search(string text)
        {
            VisibleUsers.Clear();
            text = text.ToLower();
            foreach (var user in AllUsers)
            {
                if (SearchByFirstName ?
                    user.FirstName.ToLower().StartsWith(text) :
                    user.LastName.ToLower().StartsWith(text))
                {
                    VisibleUsers.Add(user);
                }
            }
            SetVisibility();

        }
        private void TextChangeInSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FillVisibleUsers();
                SetVisibility();
            }
        }

        private void SetVisibility()
        {
            IsVisible = new MainPageVisibility()
            {
                Label = VisibleUsers.Count == 0,
                ListView = VisibleUsers.Count != 0,
                ActivityIndicator = false
            };
        }
    }
}
