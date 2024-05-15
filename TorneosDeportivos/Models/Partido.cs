namespace TorneosDeportivos.Models
{
    public class Partido
    {
        public int PartidoId { get; set; }
        public int EquipoLocal { get; set; }
        public string? EquipoL { get; set; }

        public int EquipoVisitante { get; set; }
        public string? EquipoV { get; set; }
        public int? EquipoGanador { get; set;}
        public string? EquipoG { get; set; }
        public int? EquipoPerdedor { get; set; }
        public string? EquipoP { get; set; }
        public string? Resultados { get; set; }
        public int TorneoId { get; set; }
        public string? Torneo { get; set; }
        public int CanchaId { get; set; }
        public string? Cancha { get; set; }
        public string? fecha { get; set; }
        public int Hora { get; set; }
        public int? ArbitroId { get; set; }
        public string? Arbitro { get; set; }
        public bool? ConfirmacionArbitro { get; set; }
        public decimal? CostoInscripcion { get; set; }
        public decimal? CostoTotal { get; set; }
        public string? Status { get; set; } 

    }
}
