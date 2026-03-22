using FluentValidation;
using Salon_Spa.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon_Spa.Application.Validations
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FullName)
                .NotEmpty().WithMessage("El nombre del empleado es requerido.")
                .MaximumLength(200).WithMessage("El nombre no puede exceder 200 caracteres.");

            RuleFor(e => e.Specialty)
                .MaximumLength(200).WithMessage("La especialidad no puede exceder 200 caracteres.")
                .When(e => !string.IsNullOrEmpty(e.Specialty));

            RuleFor(e => e.Phone)
                .MaximumLength(20).WithMessage("El teléfono no puede exceder 20 caracteres.")
                .Matches(@"^\d{3}-\d{3}-\d{4}$")
                .WithMessage("El teléfono debe tener el formato 809-555-0000.")
                .When(e => !string.IsNullOrEmpty(e.Phone));
        }
    }
}
