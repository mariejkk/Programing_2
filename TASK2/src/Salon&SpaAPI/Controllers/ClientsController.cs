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
    public class ClientsController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientsController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _clientService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _clientService.GetById(id);
            if (result == null) return NotFound(new { message = "Cliente no encontrado." });
            return Ok(result);
        }

        [HttpGet("by-phone/{phone}")]
        public IActionResult GetByPhone(string phone)
        {
            var result = _clientService.GetByPhone(phone);
            if (result == null) return NotFound(new { message = "Cliente no encontrado." });
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(ClientDto request)
        {
            var (isValid, errors, id) = _clientService.Create(request);
            if (!isValid) return BadRequest(new { errors });
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ClientDto request)
        {
            var (isValid, errors) = _clientService.Update(id, request);
            if (!isValid) return BadRequest(new { errors });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var (isValid, errors) = _clientService.Delete(id);
            if (!isValid) return NotFound(new { errors });
            return NoContent();
        }
    }
}
