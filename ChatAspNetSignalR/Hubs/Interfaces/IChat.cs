using ChatAspNetSignalR.Models;

namespace ChatAspNetSignalR.Hubs.Interfaces;

public interface IChat
{
    Task EnviarMensaje(Mensaje mensaje);
    Task RecibirMensaje(Mensaje mensaje);
}
