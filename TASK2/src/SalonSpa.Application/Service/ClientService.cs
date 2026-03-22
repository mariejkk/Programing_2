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
    public class ClientService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ClientValidator _validator;

        public ClientService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = new ClientValidator();
        }

        public List<ClientDto> GetAll()
        {
            var clients = _unitOfWork.ClientRepository.GetActiveClients();
            return _mapper.Map<List<ClientDto>>(clients);
        }

        public ClientDto? GetById(int id)
        {
            var client = _unitOfWork.ClientRepository.GetById(id);
            if (client == null) return null;
            return _mapper.Map<ClientDto>(client);
        }

        public ClientDto? GetByPhone(string phone)
        {
            var client = _unitOfWork.ClientRepository.GetByPhone(phone);
            if (client == null) return null;
            return _mapper.Map<ClientDto>(client);
        }

        public (bool IsValid, List<string> Errors, int Id) Create(ClientDto dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors, 0);
            }

            var client = _mapper.Map<Client>(dto);
            _unitOfWork.ClientRepository.Add(client);
            _unitOfWork.Complete();

            return (true, new List<string>(), client.Id);
        }

        public (bool IsValid, List<string> Errors) Update(int id, ClientDto dto)
        {
            var existing = _unitOfWork.ClientRepository.GetById(id);
            if (existing == null)
                return (false, new List<string> { "Cliente no encontrado." });

            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
                return (false, errors);
            }

            existing.FullName = dto.FullName;
            existing.Phone = dto.Phone;
            existing.Email = dto.Email;
            existing.IsActive = dto.IsActive;

            _unitOfWork.ClientRepository.Update(existing);
            _unitOfWork.Complete();

            return (true, new List<string>());
        }

        public (bool IsValid, List<string> Errors) Delete(int id)
        {
            var existing = _unitOfWork.ClientRepository.GetById(id);
            if (existing == null)
                return (false, new List<string> { "Cliente no encontrado." });

            _unitOfWork.ClientRepository.Delete(id);
            _unitOfWork.Complete();

            return (true, new List<string>());
        }
    }
}
