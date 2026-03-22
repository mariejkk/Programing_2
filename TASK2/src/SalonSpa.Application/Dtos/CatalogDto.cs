namespace Salon_Spa.Application.Dtos
{
    public class AppointmentStatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class PaymentMethodDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}







