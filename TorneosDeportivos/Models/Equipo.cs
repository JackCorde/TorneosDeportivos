namespace TorneosDeportivos.Models
{
    public class Equipo
    {
        public int EquipoId { get; set; }
        public string? EquipoNombre { get; set; }
        public int? DeporteId { get; set;}
        public string? Deporte { get; set; }
        public int? CategoriaId { get; set; }
        public string? Categoria { get; set; }
        public int? CoachId { get; set; }
        public string? CoachNombre { get; set; }
        public int? TorneoActualId { get; set; }
        public string? TorneoActual { get; set; }
    }
}
