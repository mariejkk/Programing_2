using Microsoft.AspNetCore.Mvc;
using SalonSpa.Domain.Entities;
using SalonSpa.Persistence;


namespace SalonSpa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentStatusesController : BaseCatalogController<AppointmentStatus>
    {
        public AppointmentStatusesController(SalonSpaContext context) : base(context) { }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentMethodsController : BaseCatalogController<PaymentMethod>
    {
        public PaymentMethodsController(SalonSpaContext context) : base(context) { }
    }
}







