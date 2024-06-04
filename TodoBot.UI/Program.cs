using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TodoBot.UI;

public class Program
{
    [STAThread]
    public static void Main()
    {
        var builder = Host.CreateApplicationBuilder ();

        builder.Services.AddHttpClient<TodoApiClient> (client => client.BaseAddress = new ($"http://localhost:333"));
        builder.Services.AddSingleton<App> ();
        builder.Services.AddSingleton<MainWindow> ();

        var appHost = builder.Build ();
        var app = appHost.Services.GetRequiredService<App> ();
        var mainWindow = appHost.Services.GetRequiredService<MainWindow> ();

        appHost.Start ();
        app.Run (mainWindow);
    }
}
