using FluentValidation;
using Salon_Spa.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon_Spa.Application.Validations
{
    public class AppointmentValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentValidator()
        {
            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("La fecha de la cita es requerida.")
                .GreaterThan(DateTime.Now).WithMessage("La fecha debe ser mayor a la fecha actual.");

            RuleFor(a => a.ClientId)
                .GreaterThan(0).WithMessage("El cliente es requerido.");

            RuleFor(a => a.EmployeeId)
                .GreaterThan(0).WithMessage("El empleado es requerido.");

            RuleFor(a => a.ServiceVariantId)
                .GreaterThan(0).WithMessage("El servicio es requerido.");

            RuleFor(a => a.AppointmentStatusId)
                .GreaterThan(0).WithMessage("El estado de la cita es requerido.");

            RuleFor(a => a.Notes)
                .MaximumLength(500).WithMessage("Las notas no pueden exceder 500 caracteres.")
                .When(a => !string.IsNullOrEmpty(a.Notes));

            RuleFor(a => a.FinalPrice)
                .GreaterThan(0).WithMessage("El precio final debe ser mayor a 0.")
                .When(a => a.FinalPrice.HasValue);
        }
    }
}