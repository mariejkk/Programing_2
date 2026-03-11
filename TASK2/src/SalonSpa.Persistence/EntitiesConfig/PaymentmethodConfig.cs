using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalonSpa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonSpa.Persistence.EntitiesConfiguration
{
    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new PaymentMethod { Id = 1, Name = "Efectivo", IsActive = true },
                new PaymentMethod { Id = 2, Name = "Tarjeta", IsActive = true },
                new PaymentMethod { Id = 3, Name = "Transferencia", IsActive = true },
                new PaymentMethod { Id = 4, Name = "Vale / Cortesía", IsActive = true }
            );
        }
    }
}







