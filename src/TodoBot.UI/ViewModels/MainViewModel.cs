
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;

namespace TodoBot.UI.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private HubConnection connection;
    public MainViewModel()
    {
        connection = new HubConnectionBuilder ()
            .WithUrl ("http://localhost:555/chatHub")
            .Build ();

        connection.Closed += async (error) =>
        {
            await Task.Delay (new Random ().Next (0, 5) * 1000);
            await connection.StartAsync ();
        };
        connection.On<string, string> ("Notify", (user, message) =>
        {

        });
    }
    [RelayCommand]
    private async Task Open()
    {
        try
        {
            await connection.StartAsync ();
        }
        catch (Exception ex)
        {
        }
    }

    [RelayCommand]
    private async Task ShowMessage()
    {
        HttpClient httpClient = new HttpClient ()
        {
            BaseAddress = new Uri("http://localhost:555")
        };
        await httpClient.PostAsync ("/api/todo", null);
    }
}
