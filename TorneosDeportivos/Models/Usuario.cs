namespace TorneosDeportivos.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string? Username { get; set; } = null;
        public string? Nombre { get; set; } = null;
        public string? Email { get; set; } = null;
        public int RolId { get; set; }
        public string? RolName { get; set; } = null;
        public string? Clave { get; set; } = null;
        public DateTime? UltimaActualizacion { get; set; }
    }
}
