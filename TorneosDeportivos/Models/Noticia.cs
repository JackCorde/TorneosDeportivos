namespace TorneosDeportivos.Models
{
    public class Noticia
    {
        public int NoticiaId { get; set; }
        public string? Encabezado { get; set; }
        public string? Contenido { get; set; }
        public int DeporteId { get; set; }
        public int TorneoId { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
