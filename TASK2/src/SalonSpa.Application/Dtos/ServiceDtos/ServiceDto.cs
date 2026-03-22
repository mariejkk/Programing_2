<<<<<<< Updated upstream:TASK2/src/Salon&SpaAPI/Models/Dtos/ServiceDTO.cs
﻿namespace Salon_SpaAPI.Models.Dtos
=======
﻿using System.ComponentModel.DataAnnotations;

namespace Salon_Spa.Application.Dtos
>>>>>>> Stashed changes:TASK2/src/SalonSpa.Application/Dtos/ServiceDtos/ServiceDto.cs
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
