using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Data.Interface
{
    public interface ICaseRepository
    {
        Task<IEnumerable<CaseDTO>> GetAllAsync();
        Task<CaseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(CaseDTO @case);
    }
}
