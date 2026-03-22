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
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(a => a.Date)
                .IsRequired();

            builder.Property(a => a.Notes)
                .HasMaxLength(500);

            builder.Property(a => a.FinalPrice)
                .HasColumnType("decimal(10,2)");

            builder.HasOne(a => a.ServiceVariant)
                .WithMany()
                .HasForeignKey(a => a.ServiceVariantId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.AppointmentStatus)
                .WithMany()
                .HasForeignKey(a => a.AppointmentStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.PaymentMethod)
                .WithMany()
                .HasForeignKey(a => a.PaymentMethodId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}