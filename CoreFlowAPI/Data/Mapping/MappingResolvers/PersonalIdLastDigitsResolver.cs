using AutoMapper;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Mapping.MappingResolvers
{
    public class PersonalIdLastDigitsResolver : IValueResolver<EmployeeDTO, Employee, int>
    {
        public int Resolve(EmployeeDTO source, Employee destination, int destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PersonalId))
                return 0;

            var parts = source.PersonalId.Split('-');
            return parts.Length > 1 && int.TryParse(parts[1], out var n) ? n : 0;
        }
    }
}
