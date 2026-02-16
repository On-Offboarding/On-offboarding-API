using AutoMapper;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Mapping
{
    public class ProfileSystemAccessConverter : Profile
    {
        public ProfileSystemAccessConverter()
        {
            CreateMap<ProfileSystemAccessDTO, SystemAccessProfile>()
                .ReverseMap();
        }
    }
}
