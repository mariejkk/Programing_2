using SalonSpa.Domain.Entities;
using SalonSpa.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonSpa.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>
    {
        private readonly SalonSpaContext _context;

        public EmployeeRepository(SalonSpaContext context) : base(context)
        {
            _context = context;
        }

        public List<Employee> GetActiveEmployees()
        {
            return _context.Employees
                .Where(e => e.IsActive)
                .OrderBy(e => e.FullName)
                .ToList();
        }

        public List<Employee> GetBySpecialty(string specialty)
        {
            return _context.Employees
                .Where(e => e.IsActive &&
                            e.Specialty != null &&
                            e.Specialty.ToLower().Contains(specialty.ToLower()))
                .ToList();
        }
    }
}