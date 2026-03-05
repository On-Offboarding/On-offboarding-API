using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Data.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();
    }
}
