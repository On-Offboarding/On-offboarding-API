using AutoMapper;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Mapping.MappingResolvers
{
    public class PersonalIdLastDigitsResolver : IValueResolver<object, Employee, string>
    {
        public string Resolve(object source, Employee destination, string destMember, ResolutionContext context)
        {
            string? personalId = source switch
            {
                EmployeeDTO dto => dto.PersonalId,
                CreateEmployeeDTO dto => dto.PersonalId,
                _ => null
            };

            if (string.IsNullOrEmpty(personalId))
                return "0000";

            var parts = personalId.Split('-');
            return parts.Length > 1 ? parts[1] : "0000";
        }
    }
}
