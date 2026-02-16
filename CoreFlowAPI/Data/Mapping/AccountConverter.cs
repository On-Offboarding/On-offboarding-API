using AutoMapper;
using CoreFlowAPI.Data.Mapping.TypeConverters;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Mapping
{
    public class AccountConverter : Profile
    {
        public AccountConverter() 
        {
            CreateMap<int, StatusOfAccount>()
                .ConvertUsing<IntToStatusOfAccountConverter>();

            CreateMap<AccountDTO, Account>();

            CreateMap<Account, AccountDTO>();
        }
    }
}
