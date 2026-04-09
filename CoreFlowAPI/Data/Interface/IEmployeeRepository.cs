using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDTO>> GetAllAsync();

        Task<EmployeeDTO?> GetByIdAsync(int id);

        Task<bool> DeleteEmployeesAccordingToGDPR(DateTime? endDate = null);
    }
}
