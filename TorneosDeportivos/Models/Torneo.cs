namespace TorneosDeportivos.Models
{
    public class Torneo
    {
        public int TorneoId { get; set; }
        public string? TorneoNombre { get; set; }
        public int? DeporteId { get; set; }
        public string? Deporte { get; set; }
        public int? CategoriaId { get; set; }
        public string? Categoria { get; set; }
        public string? fechaInicio { get; set; }
        public string? fechaFinal { get; set; }
        public int? GanadorId { get; set; }
        public string? Ganador { get; set; } = null;
    }
}
