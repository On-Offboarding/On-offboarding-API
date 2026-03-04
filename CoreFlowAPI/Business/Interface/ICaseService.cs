using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;

namespace CoreFlowAPI.Business.Interface
{
    public interface ICaseService 
    {
        Task<IEnumerable<CaseDTO>> GetAllAsync();
        Task<IEnumerable<CaseDTO>> GetAllAsync(StatusOfCase status);
        Task<CaseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(CaseDTO @case);
    }
}
