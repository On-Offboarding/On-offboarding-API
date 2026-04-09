using AutoMapper;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Mapping.MappingResolvers
{
    public class BirthDatePartResolver : IValueResolver<object, Employee, int>
    {
        public int Resolve(object source, Employee destination, int destMember, ResolutionContext context)
        {
            if(source is EmployeeDTO)
            {
                var model = source as EmployeeDTO;
                if(model != null)
                {
                    if (string.IsNullOrEmpty(model.PersonalId))
                        return 0000;

                    var parts = model.PersonalId.Split('-');
                    return int.TryParse(parts[0], out var n) ? n : 0;
                }
                
            }
            if (source is CreateEmployeeDTO)
            {
                var model = source as CreateEmployeeDTO;
                if (model != null)
                {
                    if (string.IsNullOrEmpty(model.PersonalId))
                        return 0000;

                    var parts = model.PersonalId.Split('-');
                    return int.TryParse(parts[0], out var n) ? n : 0;
                }
            }
            return 0000;
        }
    }
}
