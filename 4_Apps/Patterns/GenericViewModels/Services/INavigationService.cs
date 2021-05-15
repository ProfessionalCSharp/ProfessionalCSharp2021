using System.Threading.Tasks;

namespace GenericViewModels.Services
{
    public interface INavigationService
    {
        bool UseNavigation { get; set; }
        Task NavigateToAsync(string pageName);
        Task GoBackAsync();
        string CurrentPage { get; }
    }
}
