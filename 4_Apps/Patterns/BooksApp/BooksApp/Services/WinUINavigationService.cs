using BooksLib.Events;
using GenericViewModels.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApp.Services
{
    public class WinUINavigationService : ObservableObject, INavigationService, IRecipient<NavigationMessage>
    {
        private readonly WinUIInitializeNavigationService _initializeNavigation;

        public WinUINavigationService(WinUIInitializeNavigationService initializeNavigationService)
        {
            _initializeNavigation = initializeNavigationService;
            WeakReferenceMessenger.Default.Register<NavigationMessage>(this);
        }

        private bool _useNavigation;
        public bool UseNavigation
        {
            get => _useNavigation;
            set => SetProperty(ref _useNavigation, value);
        }

        private string _currentPage = string.Empty;
        public string CurrentPage => _currentPage;

        private Frame? _frame;
        private Frame Frame => _frame ??= _initializeNavigation.Frame;

        private Dictionary<string, Type>? _pages;
        private Dictionary<string, Type> Pages => _pages ??= _initializeNavigation.Pages;

        public Task GoBackAsync()
        {
            PageStackEntry stackEntry = Frame.BackStack.Last();
            Type backPageType = stackEntry.SourcePageType;
            var pageEntry = Pages.FirstOrDefault(pair => pair.Value == backPageType);
            _currentPage = pageEntry.Key;

            Frame.GoBack();
            return Task.CompletedTask;
        }

        public Task NavigateToAsync(string pageName)
        {
            _currentPage = pageName;
            Frame.Navigate(Pages[pageName]);
            return Task.CompletedTask;
        }

        public void Receive(NavigationMessage message)
        {
            UseNavigation = message.Value.UseNavigation;
        }
    }
}
