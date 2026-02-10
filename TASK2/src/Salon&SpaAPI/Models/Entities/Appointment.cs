

namespace Salon_SpaAPI.Models.Entities
{
    public class Appointment
    {
  
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

    }
}
