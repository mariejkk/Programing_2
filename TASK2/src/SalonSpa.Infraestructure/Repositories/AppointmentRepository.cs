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
    public class AppointmentRepository : GenericRepository<Appointment>
    {
        private readonly SalonSpaContext _context;

        public AppointmentRepository(SalonSpaContext context) : base(context)
        {
            _context = context;
        }

       
        public Appointment? GetByIdWithDetails(int id)
        {
            return _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Employee)
                .Include(a => a.ServiceVariant).ThenInclude(v => v.Service)
                .Include(a => a.AppointmentStatus)
                .Include(a => a.PaymentMethod)
                .FirstOrDefault(a => a.Id == id);
        }

        
        public List<Appointment> GetByClient(int clientId)
        {
            return _context.Appointments
                .Include(a => a.ServiceVariant).ThenInclude(v => v.Service)
                .Include(a => a.Employee)
                .Include(a => a.AppointmentStatus)
                .Where(a => a.ClientId == clientId)
                .OrderByDescending(a => a.Date)
                .ToList();
        }

        
        public List<Appointment> GetByEmployee(int employeeId)
        {
            return _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.ServiceVariant).ThenInclude(v => v.Service)
                .Include(a => a.AppointmentStatus)
                .Where(a => a.EmployeeId == employeeId)
                .OrderByDescending(a => a.Date)
                .ToList();
        }

        
        public List<Appointment> GetByDate(DateTime date)
        {
            return _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Employee)
                .Include(a => a.ServiceVariant).ThenInclude(v => v.Service)
                .Include(a => a.AppointmentStatus)
                .Where(a => a.Date.Date == date.Date)
                .OrderBy(a => a.Date)
                .ToList();
        }

       
        public List<Appointment> GetByDateRange(DateTime from, DateTime to)
        {
            return _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Employee)
                .Include(a => a.ServiceVariant).ThenInclude(v => v.Service)
                .Include(a => a.AppointmentStatus)
                .Where(a => a.Date >= from && a.Date <= to)
                .OrderBy(a => a.Date)
                .ToList();
        }

        
        public List<Appointment> GetByStatus(int statusId)
        {
            return _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Employee)
                .Include(a => a.ServiceVariant).ThenInclude(v => v.Service)
                .Where(a => a.AppointmentStatusId == statusId)
                .OrderBy(a => a.Date)
                .ToList();
        }
    }
}