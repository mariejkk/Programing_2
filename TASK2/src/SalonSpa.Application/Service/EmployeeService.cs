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
    public class EmployeeService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly EmployeeValidator _validator;

        public EmployeeService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = new EmployeeValidator();
        }

        public List<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetActiveEmployees();
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public EmployeeDto? GetById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null) return null;
            return _mapper.Map<EmployeeDto>(employee);
        }

        public List<EmployeeDto> GetBySpecialty(string specialty)
        {
            var employees = _unitOfWork.EmployeeRepository.GetBySpecialty(specialty);
            return _mapper.Map<List<EmployeeDto>>(employees);
        }

        public (bool IsValid, List<string> Errors, int Id) Create(EmployeeDto dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors, 0);
            }

            var employee = _mapper.Map<Employee>(dto);
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();

            return (true, new List<string>(), employee.Id);
        }

        public (bool IsValid, List<string> Errors) Update(int id, EmployeeDto dto)
        {
            var existing = _unitOfWork.EmployeeRepository.GetById(id);
            if (existing == null)
                return (false, new List<string> { "Empleado no encontrado." });

            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }

            existing.FullName = dto.FullName;
            existing.Specialty = dto.Specialty;
            existing.Phone = dto.Phone;
            existing.IsActive = dto.IsActive;

            _unitOfWork.EmployeeRepository.Update(existing);
            _unitOfWork.Complete();

            return (true, new List<string>());
        }

        public (bool IsValid, List<string> Errors) Delete(int id)
        {
            var existing = _unitOfWork.EmployeeRepository.GetById(id);
            if (existing == null)
                return (false, new List<string> { "Empleado no encontrado." });

            _unitOfWork.EmployeeRepository.Delete(id);
            _unitOfWork.Complete();

            return (true, new List<string>());
        }
    }
}