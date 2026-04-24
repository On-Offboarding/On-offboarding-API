using AutoMapper;
using CoreFlowAPI.Data.Mapping.MappingResolvers;
using CoreFlowAPI.Data.Mapping.TypeConverters;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;
namespace CoreFlowAPI.Data.Mapping
{
    public class EmployeeConverter : Profile
    { 
        public EmployeeConverter() 
        {

            CreateMap<EmployeeDTO, Employee>()
                .ForMember(
                dest => dest.PersonalId,
                opt => opt.MapFrom<BirthDatePartResolver>())
                .ForMember(
                dest => dest.PersonalIdLastDigits,
                opt => opt.MapFrom<PersonalIdLastDigitsResolver>());

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.PersonalId, opt => opt.MapFrom(x => x.FullPersonalId))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeHelper.ToSwedishTime(src.StartDate)))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTimeHelper.ToSwedishTime(src.EndDate)))
                .ForMember(dest => dest.DateOfEmployment, opt => opt.MapFrom(src => DateTimeHelper.ToSwedishTime(src.DateOfEmployment)));

            CreateMap<EmployeeDTO, CreateEmployeeDTO>();

        }
    }
}

 