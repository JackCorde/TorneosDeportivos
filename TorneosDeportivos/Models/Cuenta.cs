namespace TorneosDeportivos.Models
{
    public class Cuenta
    {
        public int CuentaId { get; set; }
        public int TorneoId { get; set; }
        public string? Torneo { get; set; }
        public string? Contador { get; set; }
        public int? Retiros { get; set; }
        public int? Depositos { get; set; }
        public decimal? Total { get; set; }
        public int ContadorResponsable { get; set; }

    }
}
