using BooksApp.Views;
using BooksLib.Services;
using GenericViewModels.Services;
using GenericViewModels.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Dictionary<string, Type> _pages = new()
        {
            [PageNames.BooksPage] = typeof(BooksPage),
            [PageNames.BookDetailPage] = typeof(BookDetailPage)
        };

        private readonly INavigationService _navigationService;
        // private readonly UWPInitializeNavigationService _initializeNavigationService;
        public MainWindowViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
           //  _initializeNavigationService = initializeNavigationService ?? throw new ArgumentNullException(nameof(initializeNavigationService));
        }

       // public void SetNavigationFrame(Frame frame) => _initializeNavigationService.Initialize(frame, _pages);

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
