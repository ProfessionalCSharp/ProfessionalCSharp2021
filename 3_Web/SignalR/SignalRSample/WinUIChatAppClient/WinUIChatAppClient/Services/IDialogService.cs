using System.Threading.Tasks;

namespace ChatClient.Services
{
    public interface IDialogService
    {
        Task ShowMessageAsync(string message);
    }
}