using ChatAspNetSignalR.Hubs.Interfaces;
using ChatAspNetSignalR.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatAspNetSignalR.Hubs;

public class ChatHub : Hub<IChat>
{
    public static Dictionary<string, string> lstUsuarios { get; set; } = new Dictionary<string, string>();

    public async Task EnviarMensaje(Mensaje mensaje)
    {
        if (!string.IsNullOrEmpty(mensaje.Contenido))
        {
            await Clients.All.RecibirMensaje(mensaje);
        }
        else if (!string.IsNullOrEmpty(mensaje.Usuario))
        {
            lstUsuarios.Add(Context.ConnectionId, mensaje.Usuario);
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
        await Clients.AllExcept(Context.ConnectionId).RecibirMensaje(new Mensaje
        {
            Usuario = "Host",
            Contenido = $"{lstUsuarios[Context.ConnectionId]} salio del chat."
        });
        await base.OnDisconnectedAsync(exception);
    }
}
