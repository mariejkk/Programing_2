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
    public class UnitOfWork
    {
        private readonly SalonSpaContext _context;

        public UnitOfWork(
            SalonSpaContext context,
            ServiceRepository serviceRepository,
            ServiceVariantRepository serviceVariantRepository,
            ClientRepository clientRepository,
            EmployeeRepository employeeRepository,
            AppointmentRepository appointmentRepository,
            GenericRepository<AppointmentStatus> appointmentStatusRepository,
            GenericRepository<PaymentMethod> paymentMethodRepository)
        {
            _context = context;
            ServiceRepository = serviceRepository;
            ServiceVariantRepository = serviceVariantRepository;
            ClientRepository = clientRepository;
            EmployeeRepository = employeeRepository;
            AppointmentRepository = appointmentRepository;
            AppointmentStatusRepository = appointmentStatusRepository;
            PaymentMethodRepository = paymentMethodRepository;
        }

        public ServiceRepository ServiceRepository { get; }
        public ServiceVariantRepository ServiceVariantRepository { get; }
        public ClientRepository ClientRepository { get; }
        public EmployeeRepository EmployeeRepository { get; }
        public AppointmentRepository AppointmentRepository { get; }
        public GenericRepository<AppointmentStatus> AppointmentStatusRepository { get; }
        public GenericRepository<PaymentMethod> PaymentMethodRepository { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
    }
}







