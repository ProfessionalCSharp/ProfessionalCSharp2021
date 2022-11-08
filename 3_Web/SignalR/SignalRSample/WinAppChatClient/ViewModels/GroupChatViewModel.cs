using Microsoft.AspNetCore.SignalR.Client;
using CommunityToolkit.Mvvm.Input;

using System.Collections.ObjectModel;
using System.Windows.Input;

using WindowsAppChatClient.Services;

namespace WindowsAppChatClient.ViewModels;

public sealed class GroupChatViewModel
{
    private readonly IDialogService _dialogService;
    private readonly UrlService _urlService;
    public GroupChatViewModel(IDialogService dialogService, UrlService urlService)
    {
        _dialogService = dialogService;
        _urlService = urlService;

        ConnectCommand = new RelayCommand(OnConnect);
        SendCommand = new RelayCommand(OnSendMessage);
        EnterGroupCommand = new RelayCommand(OnEnterGroup);
        LeaveGroupCommand = new RelayCommand(OnLeaveGroup);
    }

    public string? Name { get; set; }
    public string? Message { get; set; }
    public string? NewGroup { get; set; }

    public string? SelectedGroup { get; set; }

    public ObservableCollection<string> Messages { get; } = new ObservableCollection<string>();
    public ObservableCollection<string> Groups { get; } = new ObservableCollection<string>();

    public ICommand SendCommand { get; }
    public ICommand ConnectCommand { get; }
    public ICommand EnterGroupCommand { get; }
    public ICommand LeaveGroupCommand { get; }

    private HubConnection? _hubConnection;

    public async void OnConnect()
    {
        await CloseConnectionAsync();
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_urlService.GroupAddress)
            .Build();

        _hubConnection.Closed += HubConnectionClosed;

        _hubConnection.On<string, string, string>("MessageToGroup", OnMessageReceived);

        try
        {
            await _hubConnection.StartAsync();
            await _dialogService.ShowMessageAsync("client connected");
        }
        catch (HttpRequestException ex)
        {
            await _dialogService.ShowMessageAsync(ex.Message);
        }
    }

    public async void OnSendMessage()
    {
        if (_hubConnection is null) throw new InvalidOperationException("OnConnect needs to be invoked before OnSendMessage");

        try
        {
            await _hubConnection.SendAsync("Send", SelectedGroup, Name, Message);
        }
        catch (Exception ex)
        {
            await _dialogService.ShowMessageAsync(ex.Message);
        }
    }

    public async void OnMessageReceived(string group, string name, string message)
    {
        try
        {
            Messages.Add($"{group}-{name}: {message}");
        }
        catch (Exception ex)
        {
            await _dialogService.ShowMessageAsync(ex.Message);
        }
    }

    public async void OnEnterGroup()
    {
        if (_hubConnection is null) throw new InvalidOperationException($"OnConnect needs to be invoked before {nameof(OnEnterGroup)}");

        try
        {
            if (NewGroup is not null)
            {
                await _hubConnection.InvokeAsync("AddGroup", NewGroup);
                Groups.Add(NewGroup);
                SelectedGroup = NewGroup;
            }
        }
        catch (Exception ex)
        {
            await _dialogService.ShowMessageAsync(ex.Message);
        }
    }

    public async void OnLeaveGroup()
    {
        if (_hubConnection is null) throw new InvalidOperationException($"OnConnect needs to be invoked before {OnLeaveGroup}");

        try
        {
            if (SelectedGroup is not null)
            {
                await _hubConnection.InvokeAsync("LeaveGroup", SelectedGroup);
                Groups.Remove(SelectedGroup);
            }
        }
        catch (Exception ex)
        {
            await _dialogService.ShowMessageAsync(ex.Message);
        }
    }

    private Task HubConnectionClosed(Exception? arg)
        => _dialogService.ShowMessageAsync("Hub connection closed");

    private ValueTask CloseConnectionAsync() =>
        _hubConnection?.DisposeAsync() ?? ValueTask.CompletedTask;
}
