using ChatAspNetSignalR.Hubs.Interfaces;
using ChatAspNetSignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAspNetSignalR.Hubs;

public class ChatHub : Hub<IChat>
{
    public async Task EnviarMensaje(Mensaje mensaje)
    {
        if (!string.IsNullOrEmpty(mensaje.Contenido))
        {
            await Clients.All.RecibirMensaje(mensaje);
        }
        else if (!string.IsNullOrEmpty(mensaje.Contenido))
        {
            await Clients.AllExcept(Context.ConnectionId).RecibirMensaje(new Mensaje
            {
                Usuario = mensaje.Usuario,
                Contenido = "Se ha conetdo!",
            });
        }
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).RecibirMensaje(new Mensaje
        {
            Usuario = "Host",
            Contenido = "Hola =D"
        });

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
