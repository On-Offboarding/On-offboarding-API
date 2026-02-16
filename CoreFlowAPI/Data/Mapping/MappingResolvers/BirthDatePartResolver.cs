using AutoMapper;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Mapping.MappingResolvers
{
    public class BirthDatePartResolver : IValueResolver<EmployeeDTO, Employee, int>
    {
        public int Resolve(EmployeeDTO source, Employee destination, int destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PersonalId))
                return 0;

            var parts = source.PersonalId.Split('-');
            return int.TryParse(parts[0], out var n) ? n : 0;
        }
    }

}
