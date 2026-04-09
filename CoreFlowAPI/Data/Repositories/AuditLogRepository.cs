using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        public async Task<int> Create(AuditLog log)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuditLog>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
