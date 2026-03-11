using SalonSpa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonSpa.Infrastructure.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly SalonSpaContext _context;

        public GenericRepository(SalonSpaContext context)
        {
            _context = context;
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}