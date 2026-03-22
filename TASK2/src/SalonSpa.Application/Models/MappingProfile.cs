using AutoMapper;
using Salon_Spa.Application.Dtos;
using SalonSpa.Domain.Entities;

namespace SalonSpa.Application.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppointmentStatus, AppointmentStatusDto>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodDto>().ReverseMap();

            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<ServiceVariant, ServiceVariantDto>().ReverseMap();

            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<AppointmentDto, Appointment>().ReverseMap();

            CreateMap<Appointment, AppointmentDetailDto>()
                .ForMember(d => d.ClientName,
                    opt => opt.MapFrom(src => src.Client.FullName))
                .ForMember(d => d.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(d => d.ServiceName,
                    opt => opt.MapFrom(src => src.ServiceVariant.Service.Name))
                .ForMember(d => d.VariantName,
                    opt => opt.MapFrom(src => src.ServiceVariant.Name))
                .ForMember(d => d.DurationMinutes,
                    opt => opt.MapFrom(src => src.ServiceVariant.DurationMinutes))
                .ForMember(d => d.Status,
                    opt => opt.MapFrom(src => src.AppointmentStatus.Name))
                .ForMember(d => d.PaymentMethod,
                    opt => opt.MapFrom(src => src.PaymentMethod != null ? src.PaymentMethod.Name : null));
        }
    }
}







