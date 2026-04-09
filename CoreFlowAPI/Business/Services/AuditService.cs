using CoreFlowAPI.Business.Interface;
using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Services
{
    public class AuditService : IAuditService
    {
        public Task<int> Create(AuditLogDTO logDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<AuditLogDTO>> GetAllAudits()
        {
            throw new NotImplementedException();
        }
    }
}
