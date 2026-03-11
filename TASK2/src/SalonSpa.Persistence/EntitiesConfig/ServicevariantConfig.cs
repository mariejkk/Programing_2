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
    public class ServiceVariantConfiguration : IEntityTypeConfiguration<ServiceVariant>
    {
        public void Configure(EntityTypeBuilder<ServiceVariant> builder)
        {
            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(v => v.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(v => v.DurationMinutes)
                .IsRequired();
        }
    }
}