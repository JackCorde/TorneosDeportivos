namespace TorneosDeportivos.Models
{
    public class DisponibilidadArbitro
    {
        public int DisponibilidadId { get; set; }
        public string? Dia { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraSalida { get; set; }
        public decimal CostoPorPartido { get; set; }
        public int ArbitroId { get; set; }

    }
}
