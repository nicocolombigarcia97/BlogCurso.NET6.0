namespace AppWeb1.Models;

public class Publicacion 
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Subtitulo { get; set; } = string.Empty;
    public string Creador { get; set; } = string.Empty;
    public string Cuerpo { get; set; } = string.Empty;
    public DateTime Creacion { get; set; }
    public string Imagen { get; set; } = string.Empty;

}
