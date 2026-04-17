using Microsoft.EntityFrameworkCore;
using Ticketing.Models;

namespace Ticketing.Data
{
    public class SistemaTicketingContext : DbContext
    {
        public SistemaTicketingContext(DbContextOptions<SistemaTicketingContext> options) 
            : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Butaca> Butacas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<AuditoriaReserva> AuditoriasReservas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Siempre es buena práctica llamar al base primero
            base.OnModelCreating(modelBuilder);

            // Configuración de Claves Primarias y Validaciones mediante Fluent API

            // Evento
            modelBuilder.Entity<Evento>()
                .HasKey(e => e.Id);
            
            modelBuilder.Entity<Evento>()
                .Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(150); // Validación básica adicional sugerida

            // Butaca
            modelBuilder.Entity<Butaca>()
                .HasKey(b => b.Id);

            // Reserva
            modelBuilder.Entity<Reserva>()
                .HasKey(r => r.Id);

            // AuditoriaReserva
            modelBuilder.Entity<AuditoriaReserva>()
                .HasKey(a => a.Id);

            // Usuario
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.Id);
            
            modelBuilder.Entity<Usuario>()
                .Property(u => u.GoogleSubjectId)
                .IsRequired()
                .HasMaxLength(255);
                
            // (Opcional) Definir relaciones explícitamente.
            // Aunque por convención EF Core ya las entendería por las propiedades de navegación,
            // si el modelo se vuelve complejo está bueno dejarlas asentadas:
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Butaca)
                .WithMany()
                .HasForeignKey(r => r.ButacaId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<AuditoriaReserva>()
                .HasOne(a => a.Butaca)
                .WithMany()
                .HasForeignKey(a => a.ButacaId);
        }
    }
}
