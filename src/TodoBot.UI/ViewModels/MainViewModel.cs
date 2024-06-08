
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TodoBot.Core;

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
            new ToastContentBuilder ()
                .AddText (DateTime.Now.ToString())
                .AddText (message)
                .Show (); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater
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
        using StringContent jsonContent = new (
                        JsonSerializer.Serialize (new
                        {
                            Message = "hihi"
                        }),
                        Encoding.UTF8,
                        "application/json");

        await httpClient.PostAsync ("/api/todo", jsonContent);
    }
}
