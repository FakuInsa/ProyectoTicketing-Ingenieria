using System;

namespace Ticketing.Models
{
    public class AuditoriaReserva
    {
        public int Id { get; set; }
        
        public int ButacaId { get; set; }
        // Propiedad de navegación sugerida por Entity Framework Core
        public Butaca? Butaca { get; set; }
        
        // Referencia al ID real del usuario
        public int UsuarioId { get; set; }
        
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
