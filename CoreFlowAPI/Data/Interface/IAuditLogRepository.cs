using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Interface
{
    public interface IAuditLogRepository
    {
        Task<int> Create(AuditLog log);
        Task<IEnumerable<AuditLog>> GetAll();

   
        
    }
}
