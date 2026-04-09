using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Interface
{
    public interface IAuditLogService
    {
        Task<List<AuditLogViewDTO>> GetAllAuditsAsync();
        Task<int> CreateCaseAsync(CaseDTO dTO);
        Task<int> UpdateCaseAsync(CaseDTO dTO);
        
    }
}
