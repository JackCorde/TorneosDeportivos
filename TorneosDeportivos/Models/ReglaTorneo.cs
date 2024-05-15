namespace TorneosDeportivos.Models
{
    public class ReglaTorneo
    {
        public int ReglaTorneoId { get; set; }
        public string? Regla { get; set; }
        public string? Descripcion { get; set; }
        public int TorneoId { get; set; }
    }
}
