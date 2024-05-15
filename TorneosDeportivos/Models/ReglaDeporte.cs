namespace TorneosDeportivos.Models
{
    public class ReglaDeporte
    {
        public int ReglaDeporteId { get; set; }
        public string? Regla { get; set; }
        public string? Descripcion { get; set; }
        public int DeporteId { get; set; }

    }
}
