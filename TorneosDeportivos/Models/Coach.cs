namespace TorneosDeportivos.Models
{
    public class Coach
    {
        public int CoachId { get; set; }    
        public int UsuarioId { get; set;}
        public int DeporteId { get; set; }
        public int EquipoId { get; set;}
    }
}
