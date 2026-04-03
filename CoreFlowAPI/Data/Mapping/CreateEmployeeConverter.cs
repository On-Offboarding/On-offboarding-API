using AutoMapper;
using CoreFlowAPI.Data.Mapping.MappingResolvers;
using CoreFlowAPI.Data.Mapping.TypeConverters;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;
namespace CoreFlowAPI.Data.Mapping
{
    public class CreateEmployeeConverter : Profile
    { 
        public CreateEmployeeConverter() 
        {

            CreateMap<CreateEmployeeDTO, Employee>()
                .ForMember(
                dest => dest.PersonalId,
                opt => opt.MapFrom<BirthDatePartResolver>())
                .ForMember(
                dest => dest.PersonalIdLastDigits,
                opt => opt.MapFrom<PersonalIdLastDigitsResolver>());

            CreateMap<Employee, CreateEmployeeDTO>()
                .ForMember(
                dest => dest.PersonalId,
                opt => opt.MapFrom(x => x.FullPersonalId));


                
            
        }
    }
}

 