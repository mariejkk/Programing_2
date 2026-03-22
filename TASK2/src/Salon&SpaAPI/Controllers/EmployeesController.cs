using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalonSpa.Domain.Entities;
using SalonSpa.Infrastructure.Repositories;
using SalonSpa.API.Models.Dtos;

namespace SalonSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public EmployeesController(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var employees = _unitOfWork.EmployeeRepository.GetActiveEmployees();
            return Ok(_mapper.Map<List<EmployeeDto>>(employees));
        }

        [HttpGet("{id}")]
        public ApiResponse<EmployeeDto> GetById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee == null)
                return ApiResponse<EmployeeDto>.FailureResponse("Empleado no encontrado.", 404);

            return ApiResponse<EmployeeDto>.SuccessResponse(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpGet("by-specialty/{specialty}")]
        public IActionResult GetBySpecialty(string specialty)
        {
            var employees = _unitOfWork.EmployeeRepository.GetBySpecialty(specialty);
            return Ok(_mapper.Map<List<EmployeeDto>>(employees));
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName))
                return BadRequest("El nombre del empleado es requerido.");

            var employee = _mapper.Map<Employee>(request);
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();

            return Ok(new { employee.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, EmployeeDto request)
        {
            var existing = _unitOfWork.EmployeeRepository.GetById(id);
            if (existing == null) return NotFound();

            existing.FullName = request.FullName;
            existing.Specialty = request.Specialty;
            existing.Phone = request.Phone;
            existing.IsActive = request.IsActive;

            _unitOfWork.EmployeeRepository.Update(existing);
            _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _unitOfWork.EmployeeRepository.GetById(id);
            if (existing == null) return NotFound();

            _unitOfWork.EmployeeRepository.Delete(id);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}