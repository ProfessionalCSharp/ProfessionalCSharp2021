using System.Threading.Tasks;

namespace BooksLib.Services
{
    public interface IDialogService
    {
        Task ShowMessageAsync(string message);
    }
}
