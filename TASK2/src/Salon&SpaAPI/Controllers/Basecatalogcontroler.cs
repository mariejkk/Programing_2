using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonSpa.Persistence;

namespace SalonSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseCatalogController<T> : ControllerBase where T : class
    {
        protected readonly SalonSpaContext Context;
        protected readonly DbSet<T> Entity;

        public BaseCatalogController(SalonSpaContext context)
        {
            Context = context;
            Entity = context.Set<T>();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Entity.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var entity = Entity.Find(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult Create(T entity)
        {
            if (entity == null) return BadRequest("Entity cannot be null.");
            Entity.Add(entity);
            Context.SaveChanges();
            return Ok(entity);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, T entity)
        {
            if (entity == null) return BadRequest("Entity cannot be null.");
            var existing = Entity.Find(id);
            if (existing == null) return NotFound();
            Context.Entry(existing).CurrentValues.SetValues(entity);
            Context.SaveChanges();
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = Entity.Find(id);
            if (entity == null) return NotFound();
            Entity.Remove(entity);
            Context.SaveChanges();
            return NoContent();
        }
    }
}