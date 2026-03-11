using System.ComponentModel.DataAnnotations;

namespace SalonSpa.API.Models.Dtos
{
    public class ServiceDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }

    public class ServiceVariantDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int ServiceId { get; set; }
    }

    public class ServiceWithVariantsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<ServiceVariantDto> Variants { get; set; } = new();
    }

    public class CreateServiceWithVariantsDto
    {
        public ServiceDto Service { get; set; } = new();
        public List<ServiceVariantDto> Variants { get; set; } = new();
    }
}