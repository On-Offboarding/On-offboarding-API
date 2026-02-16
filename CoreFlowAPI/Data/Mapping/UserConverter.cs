using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;
using AutoMapper;

namespace CoreFlowAPI.Data.Mapping
{
    public class UserConverter : Profile
    {
        public UserConverter()
        {
            CreateMap<UserDTO, User>()
                .ReverseMap();
        }
    }
}
