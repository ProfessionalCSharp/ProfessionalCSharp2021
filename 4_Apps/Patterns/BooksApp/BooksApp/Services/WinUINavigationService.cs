using GenericViewModels.Services;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksApp.Services
{
    public class WinUINavigationService : INavigationService
    {
        private readonly WinUIInitializeNavigationService _initializeNavigation;

        public WinUINavigationService(WinUIInitializeNavigationService initializeNavigationService)
        {
            _initializeNavigation = initializeNavigationService;
        }


        public bool UseNavigation { get; set; }

        private string _currentPage = string.Empty;
        public string CurrentPage => _currentPage;

        private Frame? _frame;
        private Frame Frame => _frame ??= _initializeNavigation.Frame;

        private Dictionary<string, Type>? _pages;
        private Dictionary<string, Type> Pages => _pages ?? (_pages = _initializeNavigation.Pages);

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
    }
}
