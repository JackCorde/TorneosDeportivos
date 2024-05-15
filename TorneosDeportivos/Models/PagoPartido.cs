namespace TorneosDeportivos.Models
{
    public class PagoPartido
    {
        public int PagoPartidoId { get; set; }
        public decimal Cantidad { get; set; }
        public string? Torneo { get; set; }
        public string? EquipoVisitante { get; set; }
        public string? EquipoLocal { get; set; }
        public int PartidoId { get; set; }
        public string? fechaPago { get; set; }

        public int cuentaId { get; set; }
    }
}
