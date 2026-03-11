using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalonSpa.Domain.Entities;
using SalonSpa.Infrastructure.Repositories;
using SalonSpa.API.Models.Dtos;

namespace SalonSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public AppointmentsController(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public ApiResponse<AppointmentDetailDto> GetById(int id)
        {
            var appointment = _unitOfWork.AppointmentRepository.GetByIdWithDetails(id);
            if (appointment == null)
                return ApiResponse<AppointmentDetailDto>.FailureResponse("Cita no encontrada.", 404);

            return ApiResponse<AppointmentDetailDto>.SuccessResponse(
                _mapper.Map<AppointmentDetailDto>(appointment));
        }

        [HttpGet("by-date/{date}")]
        public IActionResult GetByDate(DateTime date)
        {
            var appointments = _unitOfWork.AppointmentRepository.GetByDate(date);
            return Ok(_mapper.Map<List<AppointmentDetailDto>>(appointments));
        }


        [HttpGet("by-client/{clientId}")]
        public IActionResult GetByClient(int clientId)
        {
            var appointments = _unitOfWork.AppointmentRepository.GetByClient(clientId);
            return Ok(_mapper.Map<List<AppointmentDetailDto>>(appointments));
        }

        [HttpGet("by-employee/{employeeId}")]
        public IActionResult GetByEmployee(int employeeId)
        {
            var appointments = _unitOfWork.AppointmentRepository.GetByEmployee(employeeId);
            return Ok(_mapper.Map<List<AppointmentDetailDto>>(appointments));
        }

        [HttpGet("by-status/{statusId}")]
        public IActionResult GetByStatus(int statusId)
        {
            var appointments = _unitOfWork.AppointmentRepository.GetByStatus(statusId);
            return Ok(_mapper.Map<List<AppointmentDetailDto>>(appointments));
        }

        [HttpPost]
        public IActionResult Create(AppointmentDto request)
        {
            var appointment = _mapper.Map<Appointment>(request);

            // Si no se especificó estado, arranca como Pendiente (Id = 1)
            if (appointment.AppointmentStatusId == 0)
                appointment.AppointmentStatusId = 1;

            _unitOfWork.AppointmentRepository.Add(appointment);
            _unitOfWork.Complete();

            return Ok(new { appointment.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AppointmentDto request)
        {
            var existing = _unitOfWork.AppointmentRepository.GetById(id);
            if (existing == null) return NotFound();

            existing.Date = request.Date;
            existing.Notes = request.Notes;
            existing.ClientId = request.ClientId;
            existing.EmployeeId = request.EmployeeId;
            existing.ServiceVariantId = request.ServiceVariantId;
            existing.AppointmentStatusId = request.AppointmentStatusId;
            existing.PaymentMethodId = request.PaymentMethodId;
            existing.FinalPrice = request.FinalPrice;

            _unitOfWork.AppointmentRepository.Update(existing);
            _unitOfWork.Complete();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _unitOfWork.AppointmentRepository.GetById(id);
            if (existing == null) return NotFound();

            _unitOfWork.AppointmentRepository.Delete(id);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}







