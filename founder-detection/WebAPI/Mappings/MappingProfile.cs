using AutoMapper;
using Model.Models;
using WebAPI.DTO;

namespace WebAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Doctor to DoctorDTO mapping
            CreateMap<Doctor, DoctorDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : "Unknown"))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User != null ? src.User.Email : ""))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User != null ? src.User.PhoneNumber : ""))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => src.Specialization ?? ""))
                .ForMember(dest => dest.Degree, opt => opt.MapFrom(src => src.Degree ?? ""))
                .ForMember(dest => dest.WorkSchedule, opt => opt.MapFrom(src => src.WorkSchedule ?? ""));

            // Services to ServiceDTO mapping
            CreateMap<Services, ServiceDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name ?? ""))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? ""))
                .ForMember(dest => dest.MethodType, opt => opt.MapFrom(src => src.MethodType ?? ""));

            // TreatmentBooking to TreatmentBookingDTO mapping
            CreateMap<TreatmentBooking, TreatmentBookingDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? ""))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.FullName : ""))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User != null ? src.User.Email : ""));

            // You can add more mappings here as needed
        }
    }
}
