using System.ComponentModel.DataAnnotations;

namespace SalonSpa.API.Models.Dtos
{
    public class AppointmentDto
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ServiceVariantId { get; set; }

        public int AppointmentStatusId { get; set; } = 1; 

        public int? PaymentMethodId { get; set; }

        public decimal? FinalPrice { get; set; }
    }

    public class AppointmentDetailDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }

        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;

        public int ServiceVariantId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string VariantName { get; set; } = string.Empty;
        public int DurationMinutes { get; set; }

        public string Status { get; set; } = string.Empty;
        public string? PaymentMethod { get; set; }
        public decimal? FinalPrice { get; set; }
    }
}







