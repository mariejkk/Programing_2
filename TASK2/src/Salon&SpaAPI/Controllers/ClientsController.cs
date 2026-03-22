using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalonSpa.Domain.Entities;
using SalonSpa.Infrastructure.Repositories;
using SalonSpa.API.Models.Dtos;

namespace SalonSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public ClientsController(IMapper mapper, UnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var clients = _unitOfWork.ClientRepository.GetActiveClients();
            return Ok(_mapper.Map<List<ClientDto>>(clients));
        }

        [HttpGet("{id}")]
        public ApiResponse<ClientDto> GetById(int id)
        {
            var client = _unitOfWork.ClientRepository.GetById(id);
            if (client == null)
                return ApiResponse<ClientDto>.FailureResponse("Cliente no encontrado.", 404);

            return ApiResponse<ClientDto>.SuccessResponse(_mapper.Map<ClientDto>(client));
        }

        [HttpGet("by-phone/{phone}")]
        public IActionResult GetByPhone(string phone)
        {
            var client = _unitOfWork.ClientRepository.GetByPhone(phone);
            if (client == null) return NotFound();
            return Ok(_mapper.Map<ClientDto>(client));
        }

        [HttpPost]
        public IActionResult Create(ClientDto request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName))
                return BadRequest("El nombre del cliente es requerido.");

            var client = _mapper.Map<Client>(request);
            _unitOfWork.ClientRepository.Add(client);
            _unitOfWork.Complete();

            return Ok(new { client.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ClientDto request)
        {
            var existing = _unitOfWork.ClientRepository.GetById(id);
            if (existing == null) return NotFound();

            existing.FullName = request.FullName;
            existing.Phone = request.Phone;
            existing.Email = request.Email;
            existing.IsActive = request.IsActive;

            _unitOfWork.ClientRepository.Update(existing);
            _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _unitOfWork.ClientRepository.GetById(id);
            if (existing == null) return NotFound();

            _unitOfWork.ClientRepository.Delete(id);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}