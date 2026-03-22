using Microsoft.AspNetCore.Mvc;
using Salon_Spa.Application.Dtos;
using Salon_Spa.Application.Services;

namespace Salon_Spa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentsController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _appointmentService.GetById(id);
            if (result == null) return NotFound(new { message = "Cita no encontrada." });
            return Ok(result);
        }

        [HttpGet("by-date/{date}")]
        public IActionResult GetByDate(DateTime date)
        {
            var result = _appointmentService.GetByDate(date);
            return Ok(result);
        }

        [HttpGet("by-client/{clientId}")]
        public IActionResult GetByClient(int clientId)
        {
            var (isValid, errors, data) = _appointmentService.GetByClient(clientId);
            if (!isValid) return NotFound(new { errors });
            return Ok(data);
        }

        [HttpGet("by-employee/{employeeId}")]
        public IActionResult GetByEmployee(int employeeId)
        {
            var (isValid, errors, data) = _appointmentService.GetByEmployee(employeeId);
            if (!isValid) return NotFound(new { errors });
            return Ok(data);
        }

        [HttpGet("by-status/{statusId}")]
        public IActionResult GetByStatus(int statusId)
        {
            var result = _appointmentService.GetByStatus(statusId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(AppointmentDto request)
        {
            var (isValid, errors, id) = _appointmentService.Create(request);
            if (!isValid) return BadRequest(new { errors });
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AppointmentDto request)
        {
            var (isValid, errors) = _appointmentService.Update(id, request);
            if (!isValid) return BadRequest(new { errors });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var (isValid, errors) = _appointmentService.Delete(id);
            if (!isValid) return NotFound(new { errors });
            return NoContent();
        }
    }
}
