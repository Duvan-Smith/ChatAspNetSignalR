using ChatAspNetSignalR.Hubs.Interfaces;
using ChatAspNetSignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAspNetSignalR.Hubs;

public class ChatHub : Hub<IChat>
{
    public async Task EnviarMensaje(Mensaje mensaje)
    {
        await Clients.All.RecibirMensaje(mensaje);
    }
}
