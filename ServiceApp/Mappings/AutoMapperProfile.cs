using AutoMapper;
using ServiceApp.DTOs;
using ServiceApp.Model;

namespace ServiceApp.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<ServiceProfessional, ServiceProfessionalDto>().ReverseMap();
            CreateMap<ClientRegistrationDto, Client>();
            CreateMap<ServiceProfessionalRegistrationDto, ServiceProfessional>();
            CreateMap<BookingHistoryDto, ServiceBooking>().ReverseMap();
        }
    }
}
