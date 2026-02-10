using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon_SpaAPI.Data;
using Salon_SpaAPI.Models.Dtos;
using Salon_SpaAPI.Models.Entities;

namespace Salon_SpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly Salon_SpaDbContext _context;

        public ServicesController(Salon_SpaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> Get()
        {
            return await _context.Services
                .Select(s => new ServiceDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Price
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDTO>> Get(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();

            return new ServiceDTO
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price
            };
        }

        [HttpPost]
        public async Task<ActionResult<ServiceDTO>> Post(ServiceDTO dto)
        {
            var service = new Service
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            dto.Id = service.Id;

            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ServiceDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();

            service.Name = dto.Name;
            service.Description = dto.Description;
            service.Price = dto.Price;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return NotFound();

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
