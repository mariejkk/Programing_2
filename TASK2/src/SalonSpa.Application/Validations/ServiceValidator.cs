using FluentValidation;
using Salon_Spa.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salon_Spa.Application.Validations
{
    public class ServiceValidator : AbstractValidator<ServiceDto>
    {
        public ServiceValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("El nombre del servicio es requerido.")
                .MaximumLength(200).WithMessage("El nombre no puede exceder 200 caracteres.");

            RuleFor(s => s.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder 500 caracteres.")
                .When(s => !string.IsNullOrEmpty(s.Description));
        }
    }

    public class ServiceVariantValidator : AbstractValidator<ServiceVariantDto>
    {
        public ServiceVariantValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("El nombre de la variante es requerido.")
                .MaximumLength(200).WithMessage("El nombre no puede exceder 200 caracteres.");

            RuleFor(v => v.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.");

            RuleFor(v => v.DurationMinutes)
                .GreaterThan(0).WithMessage("La duración debe ser mayor a 0 minutos.")
                .LessThanOrEqualTo(480).WithMessage("La duración no puede exceder 8 horas.");
        }
    }
}