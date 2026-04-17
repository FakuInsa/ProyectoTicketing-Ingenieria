using System;

namespace Ticketing.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        // ID único que envía Google (OAuth 2.0)
        public string GoogleSubjectId { get; set; } = string.Empty;
        
        public string Nombre { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
    }
}
