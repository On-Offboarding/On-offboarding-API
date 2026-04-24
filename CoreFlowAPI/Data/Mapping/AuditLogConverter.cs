using AutoMapper;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;
using CoreFlowSharedLibrary.Enums.Helpers;

namespace CoreFlowAPI.Data.Mapping
{
    public class AuditLogConverter : Profile
    {
        public AuditLogConverter()
        {
            CreateMap<AuditLog, AuditLogDTO>()
                .ReverseMap();
            CreateMap<AuditLogDTO, AuditLogViewDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => string.Format(src.Action.GetDisplayName(), src.Case.Type.ToString())))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => string.Format(src.Action.GetDescription(),src.Case.Type.ToString(),src.Case.Employee.FirstName, src.Case.Employee.LastName)))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => DateTimeHelper.ToSwedishTime(src.TimeStamp)))
                .ForMember(dest => dest.ByUser, opt => opt.MapFrom(src => string.Format("Av: {0}", src.User.Email)));
        }
    }
}
