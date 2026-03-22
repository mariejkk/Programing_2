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
    public class SalonService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ServiceValidator _serviceValidator;
        private readonly ServiceVariantValidator _variantValidator;

        public SalonService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _serviceValidator = new ServiceValidator();
            _variantValidator = new ServiceVariantValidator();
        }

        public List<ServiceWithVariantsDto> GetAllWithVariants()
        {
            return _unitOfWork.ServiceRepository.GetServicesWithVariants()
                .Select(s => new ServiceWithVariantsDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Variants = s.Variants
                        .Where(v => v.IsActive)
                        .Select(v => new ServiceVariantDto
                        {
                            Id = v.Id,
                            Name = v.Name,
                            DurationMinutes = v.DurationMinutes,
                            Price = v.Price,
                            IsActive = v.IsActive,
                            ServiceId = v.ServiceId
                        }).ToList()
                }).ToList();
        }

        public ServiceDto? GetById(int id)
        {
            var service = _unitOfWork.ServiceRepository.GetById(id);
            if (service == null) return null;
            return _mapper.Map<ServiceDto>(service);
        }

        public (bool IsValid, List<string> Errors, int Id) CreateWithVariants(CreateServiceWithVariantsDto dto)
        {
            var serviceValidation = _serviceValidator.Validate(dto.Service);
            if (!serviceValidation.IsValid)
            {
                var errors = serviceValidation.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors, 0);
            }

            foreach (var variantDto in dto.Variants)
            {
                var variantValidation = _variantValidator.Validate(variantDto);
                if (!variantValidation.IsValid)
                {
                    var errors = variantValidation.Errors.Select(e => e.ErrorMessage).ToList();
                    return (false, errors, 0);
                }
            }

            var service = _mapper.Map<Service>(dto.Service);

            _unitOfWork.BeginTransaction();
            _unitOfWork.ServiceRepository.Add(service);
            _unitOfWork.Complete();

            foreach (var variantDto in dto.Variants)
            {
                var variant = new ServiceVariant
                {
                    Name = variantDto.Name,
                    DurationMinutes = variantDto.DurationMinutes,
                    Price = variantDto.Price,
                    IsActive = true,
                    ServiceId = service.Id
                };
                _unitOfWork.ServiceVariantRepository.Add(variant);
            }

            _unitOfWork.Complete();
            _unitOfWork.CommitTransaction();

            return (true, new List<string>(), service.Id);
        }

        public (bool IsValid, List<string> Errors) Update(int id, ServiceDto dto)
        {
            var existing = _unitOfWork.ServiceRepository.GetById(id);
            if (existing == null)
                return (false, new List<string> { "Servicio no encontrado." });

            var validation = _serviceValidator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.IsActive = dto.IsActive;

            _unitOfWork.ServiceRepository.Update(existing);
            _unitOfWork.Complete();

            return (true, new List<string>());
        }

        public (bool IsValid, List<string> Errors) Delete(int id)
        {
            var existing = _unitOfWork.ServiceRepository.GetById(id);
            if (existing == null)
                return (false, new List<string> { "Servicio no encontrado." });

            _unitOfWork.ServiceRepository.Delete(id);
            _unitOfWork.Complete();

            return (true, new List<string>());
        }
    }
}