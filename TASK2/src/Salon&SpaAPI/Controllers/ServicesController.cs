using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalonSpa.Domain.Entities;
using SalonSpa.Infrastructure.Repositories;
using SalonSpa.API.Models.Dtos;

namespace SalonSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public ServicesController(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var list = _unitOfWork.ServiceRepository.GetAll();
            return Ok(_mapper.Map<List<ServiceDto>>(list));
        }

        [HttpGet("{id}")]
        public ApiResponse<ServiceDto> GetById(int id)
        {
            var service = _unitOfWork.ServiceRepository.GetById(id);
            if (service == null)
                return ApiResponse<ServiceDto>.FailureResponse("Servicio no encontrado.", 404);

            return ApiResponse<ServiceDto>.SuccessResponse(_mapper.Map<ServiceDto>(service));
        }

        [HttpGet("with-variants")]
        public IActionResult GetWithVariants()
        {
            var services = _unitOfWork.ServiceRepository.GetServicesWithVariants()
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
                            Price = v.Price
                        }).ToList()
                }).ToList();

            return Ok(services);
        }

        [HttpPost]
        public IActionResult Create(ServiceDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("El nombre del servicio es requerido.");

            var service = _mapper.Map<Service>(request);
            _unitOfWork.ServiceRepository.Add(service);
            _unitOfWork.Complete();

            return Ok(new { service.Id });
        }

        [HttpPost("create-with-variants")]
        public IActionResult CreateWithVariants(CreateServiceWithVariantsDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Service.Name))
                return BadRequest("El nombre del servicio es requerido.");

            var service = _mapper.Map<Service>(request.Service);

            _unitOfWork.BeginTransaction();
            _unitOfWork.ServiceRepository.Add(service);
            _unitOfWork.Complete();

            foreach (var variantDto in request.Variants)
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

            return Ok(new { service.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ServiceDto request)
        {
            var existing = _unitOfWork.ServiceRepository.GetById(id);
            if (existing == null) return NotFound();

            existing.Name = request.Name;
            existing.Description = request.Description;
            existing.IsActive = request.IsActive;

            _unitOfWork.ServiceRepository.Update(existing);
            _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _unitOfWork.ServiceRepository.GetById(id);
            if (existing == null) return NotFound();

            _unitOfWork.ServiceRepository.Delete(id);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}







