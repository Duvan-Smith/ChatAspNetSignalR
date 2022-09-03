namespace ChatAspNetSignalR.Models;

public class Mensaje
{
    public string Usuario { get; set; } = default!;
    public string? Contenido { get; set; }
    public string Sala { get; set; } = default!;
}
