using System;

namespace Ticketing.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int ButacaId { get; set; }
        
        // Propiedad de navegación sugerida por Entity Framework Core
        public Butaca? Butaca { get; set; }
        
        public DateTime FechaCreacion { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
