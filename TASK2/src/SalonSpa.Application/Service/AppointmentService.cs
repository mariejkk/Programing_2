using AutoMapper;
using Salon_Spa.Application.Dtos;
using SalonSpa.Domain.Entities;
using SalonSpa.Infrastructure.Repositories;
using Salon_Spa.Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon_Spa.Application.Services
{
    public class AppointmentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppointmentValidator _validator;

        public AppointmentService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = new AppointmentValidator();
        }

        public AppointmentDetailDto? GetById(int id)
        {
            var appointment = _unitOfWork.AppointmentRepository.GetByIdWithDetails(id);
            if (appointment == null) return null;
            return _mapper.Map<AppointmentDetailDto>(appointment);
        }

        public List<AppointmentDetailDto> GetByDate(DateTime date)
        {
            var appointments = _unitOfWork.AppointmentRepository.GetByDate(date);
            return _mapper.Map<List<AppointmentDetailDto>>(appointments);
        }

        public (bool IsValid, List<string> Errors, List<AppointmentDetailDto> Data) GetByClient(int clientId)
        {
            var client = _unitOfWork.ClientRepository.GetById(clientId);
            if (client == null)
                return (false, new List<string> { "Cliente no encontrado." }, new());

            var appointments = _unitOfWork.AppointmentRepository.GetByClient(clientId);
            return (true, new List<string>(), _mapper.Map<List<AppointmentDetailDto>>(appointments));
        }

        public (bool IsValid, List<string> Errors, List<AppointmentDetailDto> Data) GetByEmployee(int employeeId)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(employeeId);
            if (employee == null)
                return (false, new List<string> { "Empleado no encontrado." }, new());

            var appointments = _unitOfWork.AppointmentRepository.GetByEmployee(employeeId);
            return (true, new List<string>(), _mapper.Map<List<AppointmentDetailDto>>(appointments));
        }

        public List<AppointmentDetailDto> GetByStatus(int statusId)
        {
            var appointments = _unitOfWork.AppointmentRepository.GetByStatus(statusId);
            return _mapper.Map<List<AppointmentDetailDto>>(appointments);
        }

        public (bool IsValid, List<string> Errors, int Id) Create(AppointmentDto dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors, 0);
            }

            var appointment = _mapper.Map<Appointment>(dto);
            if (appointment.AppointmentStatusId == 0)
                appointment.AppointmentStatusId = 1;

            _unitOfWork.AppointmentRepository.Add(appointment);
            _unitOfWork.Complete();

            return (true, new List<string>(), appointment.Id);
        }

        public (bool IsValid, List<string> Errors) Update(int id, AppointmentDto dto)
        {
            var existing = _unitOfWork.AppointmentRepository.GetById(id);
            if (existing == null)
                return (false, new List<string> { "Cita no encontrada." });

            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }

            existing.Date = dto.Date;
            existing.Notes = dto.Notes;
            existing.ClientId = dto.ClientId;
            existing.EmployeeId = dto.EmployeeId;
            existing.ServiceVariantId = dto.ServiceVariantId;
            existing.AppointmentStatusId = dto.AppointmentStatusId;
            existing.PaymentMethodId = dto.PaymentMethodId;
            existing.FinalPrice = dto.FinalPrice;

            _unitOfWork.AppointmentRepository.Update(existing);
            _unitOfWork.Complete();

            return (true, new List<string>());
        }

        public (bool IsValid, List<string> Errors) Delete(int id)
        {
            var existing = _unitOfWork.AppointmentRepository.GetById(id);
            if (existing == null)
                return (false, new List<string> { "Cita no encontrada." });

            _unitOfWork.AppointmentRepository.Delete(id);
            _unitOfWork.Complete();

            return (true, new List<string>());
        }
    }
}