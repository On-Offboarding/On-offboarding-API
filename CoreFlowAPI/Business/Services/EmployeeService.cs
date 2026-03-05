using CoreFlowAPI.Business.Interface;
using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        public Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
