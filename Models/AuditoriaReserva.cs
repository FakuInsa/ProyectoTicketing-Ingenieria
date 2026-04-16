using System;

namespace Ticketing.Models
{
    public class AuditoriaReserva
    {
        public int Id { get; set; }
        
        public int ButacaId { get; set; }
        // Propiedad de navegación sugerida por Entity Framework Core
        public Butaca? Butaca { get; set; }
        
        // Generalmente en .NET Identity los IDs de usuario son strings (Guid o varchar), 
        // pero puedes cambiarlo a int si tu tabla de usuarios usa int.
        public string UsuarioId { get; set; } = string.Empty;
        
        public string Accion { get; set; } = string.Empty;
        
        public DateTime FechaHora { get; set; }
        
        public NivelLog Nivel { get; set; }
    }

    public enum NivelLog
    {
        INFO,
        WARN,
        ERROR,
        CRITICAL
    }
}
