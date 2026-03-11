using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonSpa.Domain.Entities
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public int ServiceVariantId { get; set; }
        public ServiceVariant ServiceVariant { get; set; } = null!;

        public int AppointmentStatusId { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; } = null!;

        public int? PaymentMethodId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? FinalPrice { get; set; }
    }
}







