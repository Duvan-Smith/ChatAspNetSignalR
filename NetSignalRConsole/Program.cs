using ChatAspNetSignalR.Models;
using Microsoft.AspNetCore.SignalR.Client;

var conexion = new HubConnectionBuilder().WithUrl("https://localhost:7242/chatHub").Build();

conexion.On<Mensaje>("RecibirMensaje", (mensaje) =>
    Console.WriteLine($"{mensaje.Usuario} - {mensaje.Contenido}")
);

Console.ReadLine();
