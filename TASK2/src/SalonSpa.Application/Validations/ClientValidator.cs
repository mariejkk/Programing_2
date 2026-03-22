using FluentValidation;
using Salon_Spa.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon_Spa.Application.Validations
{
    public class ClientValidator : AbstractValidator<ClientDto>
    {
        public ClientValidator()
        {
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage("El nombre del cliente es requerido.")
                .MaximumLength(200).WithMessage("El nombre no puede exceder 200 caracteres.");

            RuleFor(c => c.Phone)
                .MaximumLength(20).WithMessage("El teléfono no puede exceder 20 caracteres.")
                .Matches(@"^\d{3}-\d{3}-\d{4}$")
                .WithMessage("El teléfono debe tener el formato 809-555-0000.")
                .When(c => !string.IsNullOrEmpty(c.Phone));

            RuleFor(c => c.Email)
                .EmailAddress().WithMessage("El correo electrónico no es válido.")
                .MaximumLength(200).WithMessage("El correo no puede exceder 200 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.Email));
        }
    }
}