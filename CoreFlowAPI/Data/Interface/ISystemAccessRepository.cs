using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Interface
{
    public interface ISystemAccessRepository
    {
        Task<IEnumerable<SystemAccess>> GetAllAsync();
        Task<IEnumerable<ProfileSystemAccessDTO>> GetAllProfilesAsync();
    }
}
