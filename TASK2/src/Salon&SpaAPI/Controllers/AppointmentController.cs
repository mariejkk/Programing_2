using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon_SpaAPI.Data;
using Salon_SpaAPI.Models.Dtos;
using Salon_SpaAPI.Models.Entities;

namespace Salon_SpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly Salon_SpaDbContext _context;

        public AppointmentsController(Salon_SpaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> Get()
        {
            var appointments = await _context.Appointments
                .Select(a => new AppointmentDTO
                {
                    Id = a.Id,
                    CustomerName = a.CustomerName,
                    AppointmentDate = a.AppointmentDate,
                    ServiceId = a.ServiceId
                })
                .ToListAsync();

            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDTO>> Get(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            return new AppointmentDTO
            {
                Id = appointment.Id,
                CustomerName = appointment.CustomerName,
                AppointmentDate = appointment.AppointmentDate,
                ServiceId = appointment.ServiceId
            };
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> Post(AppointmentDTO dto)
        {
            var appointment = new Appointment
            {
                CustomerName = dto.CustomerName,
                AppointmentDate = dto.AppointmentDate,
                ServiceId = dto.ServiceId
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            dto.Id = appointment.Id;

            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AppointmentDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            appointment.CustomerName = dto.CustomerName;
            appointment.AppointmentDate = dto.AppointmentDate;
            appointment.ServiceId = dto.ServiceId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}