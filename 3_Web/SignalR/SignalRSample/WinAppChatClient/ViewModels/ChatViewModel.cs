using Microsoft.AspNetCore.SignalR.Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;

using WindowsAppChatClient.Services;
using Microsoft.UI.Dispatching;

namespace WindowsAppChatClient.ViewModels;

public class ChatViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly UrlService _urlService;
    private readonly DispatcherQueue _dispatcherQueue;
    public ChatViewModel(IDialogService dialogService, UrlService urlService)
    {
        _dialogService = dialogService;
        _urlService = urlService;
        _dispatcherQueue = DispatcherQueue.GetForCurrentThread();

        ConnectCommand = new RelayCommand(OnConnect);
        SendCommand = new RelayCommand(OnSendMessage);
    }

    public string? Name { get; set; }
    public string? Message { get; set; }
    private string? _infoText;
    public string? InfoText
    {
        get => _infoText;
        set => SetProperty(ref _infoText, value);
    }

    private bool _showInfoBar;
    public bool ShowInfoBar
    {
        get => _showInfoBar;
        set => SetProperty(ref _showInfoBar, value);
    }

    private void DisplayInfoBar(string text)
    {
        InfoText = text;
        ShowInfoBar = true;
    }

    public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();

    public RelayCommand SendCommand { get; }

    public RelayCommand ConnectCommand { get; }

    private HubConnection? _hubConnection;

    public async void OnConnect()
    {
        await CloseConnectionAsync();
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_urlService.ChatAddress)
            .Build();

        _hubConnection.Closed += HubConnectionClosed;

        _hubConnection.On<string, string>("BroadcastMessage", OnMessageReceived);

        try
        {
            await _hubConnection.StartAsync();
            InfoText = "client connected";
            DisplayInfoBar("client connected");
            // await _dialogService.ShowMessageAsync("client connected");
        }
        catch (HttpRequestException ex)
        {
            DisplayInfoBar(ex.Message);
            await _dialogService.ShowMessageAsync(ex.Message);
        }
    }

    private Task HubConnectionClosed(Exception? arg)
        => _dialogService.ShowMessageAsync("Hub connection closed");

    public async void OnSendMessage()
    {
        if (_hubConnection is null) throw new InvalidOperationException("OnConnect needs to be invoked before OnSendMessage");
        try
        {
            await _hubConnection.SendAsync("Send", Name, Message);
        }
        catch (Exception ex)
        {
            DisplayInfoBar(ex.Message);
            //await _dialogService.ShowMessageAsync(ex.Message);
        }
    }

    public void OnMessageReceived(string name, string message)
    {
        try
        {
            _dispatcherQueue.TryEnqueue(() =>
            {
                Messages.Add($"{name}: {message}");
            });

            //_dispatcherQueue.DispatcherQueue.TryEnqueue(() =>
            //{
            //    Messages.Add($"{name}: {message}");
            //});
            //await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            //{
            //    Messages.Add($"{name}: {message}");
            //});
        }
        catch (Exception ex)
        {
            DisplayInfoBar(ex.Message);
        }
    }

    private ValueTask CloseConnectionAsync()
        => _hubConnection?.DisposeAsync() ?? ValueTask.CompletedTask;
}
