using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Interface
{
    public interface IAuditService
    {
        Task<List<AuditLogDTO>> GetAllAudits();
        Task<int> Create(AuditLogDTO logDTO); 
    }
}
