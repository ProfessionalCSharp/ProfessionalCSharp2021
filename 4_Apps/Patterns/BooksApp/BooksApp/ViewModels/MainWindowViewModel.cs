using BooksApp.Services;
using BooksApp.Views;
using BooksLib.Services;
using GenericViewModels.Services;
using GenericViewModels.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;

namespace BooksApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly Dictionary<string, Type> _pages = new()
        {
            [PageNames.BooksPage] = typeof(BooksPage),
            [PageNames.BookDetailPage] = typeof(BookDetailPage)
        };

        private readonly INavigationService _navigationService;
        private readonly WinUIInitializeNavigationService _initializeNavigationService;
        public MainWindowViewModel(INavigationService navigationService, WinUIInitializeNavigationService initializeNavigationService)
        {
            _navigationService = navigationService;
            _initializeNavigationService = initializeNavigationService;
        }

        public void SetNavigationFrame(Frame frame) => _initializeNavigationService.Initialize(frame, _pages);

        public void UseNavigation(bool navigation)
        {
            _navigationService.UseNavigation = navigation;
        }

        public void OnNavigationSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is NavigationViewItem navigationItem)
            {
                switch (navigationItem.Tag)
                {
                    case "books":
                        _navigationService.NavigateToAsync(PageNames.BooksPage);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
