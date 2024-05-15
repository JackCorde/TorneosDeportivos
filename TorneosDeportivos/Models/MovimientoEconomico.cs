namespace TorneosDeportivos.Models
{
    public class MovimientoEconomico
    {
        public int MovimientoEconomicoId { get; set; }
        public string? Razon { get; set; }
        public decimal Cantidad { get; set; }
        public string? Responsable { get; set; }
        public DateTime Fecha { get; set; }
        public int CuentaId { get; set; }
        public string? TipoMovimiento { get; set; }
        public int ContadorId { get; set; }
    }
}
