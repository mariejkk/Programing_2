using Microsoft.EntityFrameworkCore;
using SalonSpa.Domain.Entities;
using SalonSpa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonSpa.Infrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Service>
    {
        private readonly SalonSpaContext _context;

        public ServiceRepository(SalonSpaContext context) : base(context)
        {
            _context = context;
        }

        public Service? GetServiceByName(string name)
        {
            return _context.Services
                .FirstOrDefault(s => s.Name.ToLower().Contains(name.ToLower()));
        }

        public List<Service> GetServicesWithVariants()
        {
            return _context.Services
                .Include(s => s.Variants)
                .Where(s => s.IsActive)
                .ToList();
        }
    }
}







