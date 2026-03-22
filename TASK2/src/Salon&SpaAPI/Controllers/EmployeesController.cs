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
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _employeeService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _employeeService.GetById(id);
            if (result == null) return NotFound(new { message = "Empleado no encontrado." });
            return Ok(result);
        }

        [HttpGet("by-specialty/{specialty}")]
        public IActionResult GetBySpecialty(string specialty)
        {
            var result = _employeeService.GetBySpecialty(specialty);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto request)
        {
            var (isValid, errors, id) = _employeeService.Create(request);
            if (!isValid) return BadRequest(new { errors });
            return Ok(new { id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, EmployeeDto request)
        {
            var (isValid, errors) = _employeeService.Update(id, request);
            if (!isValid) return BadRequest(new { errors });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var (isValid, errors) = _employeeService.Delete(id);
            if (!isValid) return NotFound(new { errors });
            return NoContent();
        }
    }
}