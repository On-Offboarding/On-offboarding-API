using AutoMapper;
using CoreFlowAPI.Data.Mapping.TypeConverters;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Mapping
{
    public class CreateCaseConverter : Profile
    {
        public CreateCaseConverter()
        {
            CreateMap<int, StatusOfCase>()
                .ConvertUsing<IntToStatusOfCaseConverter>();
            CreateMap<int, TypeOfCase>()
                .ConvertUsing<IntToTypeOfCaseConverter>();

            CreateMap<CreateCaseDTO, Case>();
            CreateMap<Case, CreateCaseDTO>();
        }
    }
}
