using Microsoft.AspNetCore.SignalR;
using System.Net;

var builder = WebApplication.CreateBuilder (args);

// Add services to the container.

builder.Services.AddControllers ();
builder.Services.AddWindowsService (options =>
{
    options.ServiceName = "todobotapi";
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.WebHost.ConfigureKestrel ((context, serverOptions) =>
{
    serverOptions.Listen (IPAddress.Loopback, 555);
});

builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();
builder.Services.AddSignalR ();
var app = builder.Build ();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment ())
{
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseAuthorization ();

app.MapControllers ();
app.MapHub<ChatHub> ("/chatHub");

app.Run ();


public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync ("Notify", user, message);
    }
}
