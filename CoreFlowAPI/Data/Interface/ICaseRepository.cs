using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Interface
{
    public interface ICaseRepository
    {
        Task<IEnumerable<CaseDTO>> GetAllAsync();
        Task<IEnumerable<CaseDTO>> GetAllAsync(StatusOfCase status);
        Task<CaseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(Case @case, Employee employee, List<Account> accounts);
    }
}
