namespace TorneosDeportivos.Models
{
    public class Cancha
    {
        public int CanchaId { get; set; }
        public string? Descripcion { get; set; }
        public bool Activa { get; set; }
        public int DeporteId { get; set; }
        public string? Deporte { get; set; }

    }
}
