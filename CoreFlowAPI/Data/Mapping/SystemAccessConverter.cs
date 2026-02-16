using AutoMapper;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Mapping
{
    public class SystemAccessConverter : Profile
    {
        public SystemAccessConverter()
        {
            CreateMap<SystemAccessDTO, SystemAccess>()
                .ReverseMap();
        }
    }
}
