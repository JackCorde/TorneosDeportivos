namespace TorneosDeportivos.Models.ViewModels
{
    public class UsuariosViewModel
    {
        public int UsuarioId { get; set; }
        public string? Username { get; set; } = null;
        public string? Nombre { get; set; } = null;
        public string? Email { get; set; } = null;
        public int RolId { get; set; }
        public string? RolName { get; set; } = null;
        public int? NumeroCuentas { get; set; }
        public string? Deporte { get;set; } = null;
        public int? NumeroPartidos { get;set; }
        public string? Equipo { get; set; } = null;
        public decimal? Costo { get; set; } = null;

    }
}
