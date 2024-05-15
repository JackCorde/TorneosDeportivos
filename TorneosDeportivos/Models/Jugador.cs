namespace TorneosDeportivos.Models
{
    public class Jugador
    {
        public int JugadorId { get; set; }
        public string? Nombre { get;set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public int EquipoId { get; set; }
        public int Edad { get; set; }
        public int NumeroJugador { get; set; }
    }
}
