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
    public class AppointmentStatusConfiguration : IEntityTypeConfiguration<AppointmentStatus>
    {
        public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
        {
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new AppointmentStatus { Id = 1, Name = "Pendiente" },
                new AppointmentStatus { Id = 2, Name = "Confirmada" },
                new AppointmentStatus { Id = 3, Name = "En curso" },
                new AppointmentStatus { Id = 4, Name = "Completada" },
                new AppointmentStatus { Id = 5, Name = "Cancelada" }
            );
        }
    }
}







