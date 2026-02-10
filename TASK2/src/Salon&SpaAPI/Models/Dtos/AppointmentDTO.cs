namespace Salon_SpaAPI.Models.Dtos
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public int ServiceId { get; set; }
    }
}
