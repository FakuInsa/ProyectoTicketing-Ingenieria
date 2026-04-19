using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticketing.Data;
using Ticketing.DTOs;
using Ticketing.Models;

namespace Ticketing.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly SistemaTicketingContext _context;

        public ReservationsController(SistemaTicketingContext context)
        {
            _context = context;
        }

        // POST: api/v1/reservations
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationRequest request)
        {
            // Validar la existencia de la butaca
            var butaca = await _context.Butacas.FirstOrDefaultAsync(b => b.Id == request.ButacaId);

            if (butaca == null)
            {
                return NotFound("La butaca especificada no existe.");
            }

            if (butaca.Estado != EstadoButaca.Disponible)
            {
                return BadRequest($"La butaca no está disponible. Estado actual: {butaca.Estado}");
            }

            // Cambiar el estado de la butaca
            butaca.Estado = EstadoButaca.Reservada;
            butaca.FechaBloqueo = DateTime.UtcNow;

            // Crear el registro de la reserva (Pendiente)
            var reserva = new Reserva
            {
                ButacaId = request.ButacaId,
                UsuarioId = request.UsuarioId,
                FechaCreacion = DateTime.UtcNow,
                Expiracion = DateTime.UtcNow.AddMinutes(5),
                Estado = "Pending"
            };

            _context.Reservas.Add(reserva);

            // Crear el registro de auditoría
            var auditoria = new Auditoria
            {
                UsuarioId = request.UsuarioId,
                Accion = "CREATE_RESERVATION",
                RecursoAfectado = "Butaca",
                RecursoId = butaca.Id,
                FechaHora = DateTime.UtcNow,
                Detalle = $"{{\"mensaje\": \"Intento de reserva creado para la butaca {butaca.Id}\"}}"
            };

            _context.Auditorias.Add(auditoria);

            try
            {
                // Guardar los cambios transaccionalmente (SaveChagesAsync maneja la transacción por defecto para múltiples adds/updates)
                await _context.SaveChangesAsync();
                
                return Ok(new { 
                    Mensaje = "Reserva creada exitosamente.",
                    ReservaId = reserva.Id,
                    ButacaId = butaca.Id,
                    Expiracion = reserva.Expiracion
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                // Ocurriría si la butaca fue modificada concurrentemente
                return Conflict("La butaca fue modificada o reservada por otro usuario en el mismo instante.");
            }
            catch (Exception ex)
            {
                // Un log robusto iría aquí
                return StatusCode(500, $"Ocurrió un error interno al crear la reserva: {ex.Message}");
            }
        }
    }
}
