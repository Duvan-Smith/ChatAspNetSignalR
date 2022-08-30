using Microsoft.AspNetCore.SignalR;

namespace ChatAspNetSignalR.Hubs;

public class ChatHub : Hub
{
    public async Task EnviarMensaje(string user, string mensaje)
    {
        await Clients.All.SendAsync("RecibirMensaje", user, mensaje);
    }
}
