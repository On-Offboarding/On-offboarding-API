using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Interface
{
    public interface IAuditLogRepository
    {
        Task<int> CreateAsync(AuditLog log);
        Task<IEnumerable<AuditLog>> GetAllAsync();

   
        
    }
}
