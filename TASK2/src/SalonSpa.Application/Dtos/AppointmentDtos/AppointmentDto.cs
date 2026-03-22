<<<<<<< Updated upstream:TASK2/src/Salon&SpaAPI/Models/Dtos/AppointmentDTO.cs
﻿namespace Salon_SpaAPI.Models.Dtos
=======
﻿using System.ComponentModel.DataAnnotations;

namespace Salon_Spa.Application.Dtos
>>>>>>> Stashed changes:TASK2/src/SalonSpa.Application/Dtos/AppointmentDtos/AppointmentDto.cs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public int ServiceId { get; set; }
    }
}
