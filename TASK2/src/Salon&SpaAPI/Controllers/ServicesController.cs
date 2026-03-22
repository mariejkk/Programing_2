using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalonSpa.Domain.Entities;
using SalonSpa.Infrastructure.Repositories;
using Salon_Spa.Application.Dtos;
using Salon_Spa.Application.Services;

namespace Salon_Spa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly SalonService _salonService;

        public ServicesController(SalonService salonService)
        {
            _salonService = salonService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _salonService.GetAllWithVariants();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _salonService.GetById(id);
            if (result == null) return NotFound(new { message = "Servicio no encontrado." });
            return Ok(result);
        }

        [HttpPost("create-with-variants")]
        public IActionResult CreateWithVariants(CreateServiceWithVariantsDto request)
        {
            var (isValid, errors, id) = _salonService.CreateWithVariants(request);
            if (!isValid) return BadRequest(new { errors });
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ServiceDto request)
        {
            var (isValid, errors) = _salonService.Update(id, request);
            if (!isValid) return BadRequest(new { errors });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var (isValid, errors) = _salonService.Delete(id);
            if (!isValid) return NotFound(new { errors });
            return NoContent();
        }
    }
}






