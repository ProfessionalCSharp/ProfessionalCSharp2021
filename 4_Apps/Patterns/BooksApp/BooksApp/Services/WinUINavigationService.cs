using GenericViewModels.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Services
{
    public class WinUINavigationService : INavigationService
    {
        public bool UseNavigation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string CurrentPage => throw new NotImplementedException();

        public Task GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync(string page)
        {
            throw new NotImplementedException();
        }
    }
}
