using SalonSpa.Domain.Entities;
using SalonSpa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonSpa.Infrastructure.Repositories
{
    public class ServiceVariantRepository : GenericRepository<ServiceVariant>
    {
        private readonly SalonSpaContext _context;

        public ServiceVariantRepository(SalonSpaContext context) : base(context)
        {
            _context = context;
        }

        public List<ServiceVariant> GetByServiceId(int serviceId)
        {
            return _context.ServiceVariants
                .Where(v => v.ServiceId == serviceId && v.IsActive)
                .ToList();
        }

        public ServiceVariant? GetVariantByName(string name)
        {
            return _context.ServiceVariants
                .FirstOrDefault(v => v.Name.ToLower().Contains(name.ToLower()));
        }
    }
}







