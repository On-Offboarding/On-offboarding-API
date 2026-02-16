using AutoMapper;
using CoreFlowAPI.Data.Mapping.TypeConverters;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Mapping
{
    public class CaseConverter : Profile
    {
        public CaseConverter() 
        {
            CreateMap<int, StatusOfCase>()
                .ConvertUsing<IntToStatusOfCaseConverter>();
            CreateMap<int, TypeOfCase>()
                .ConvertUsing<IntToTypeOfCaseConverter>();

            CreateMap<CaseDTO, Case>();
            CreateMap<Case, CaseDTO>();
          
        }
    }
}
