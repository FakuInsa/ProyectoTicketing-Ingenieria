using System;

namespace Ticketing.Models
{
    public class Butaca
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Sector { get; set; } = string.Empty;
        public EstadoButaca Estado { get; set; }
        public DateTime? FechaBloqueo { get; set; }
    }

    public enum EstadoButaca
    {
        Disponible,
        Bloqueada,
        Reservada,
        Vendida
    }
}
