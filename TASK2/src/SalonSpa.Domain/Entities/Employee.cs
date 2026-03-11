using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonSpa.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Specialty { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;

        public List<Appointment> Appointments { get; set; } = new();
    }
}







