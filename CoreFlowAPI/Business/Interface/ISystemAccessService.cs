using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Interface
{
    public interface ISystemAccessService
    {
        Task<IEnumerable<SystemAccessDTO>> GetAllAsync();
        Task<IEnumerable<ProfileSystemAccessDTO>> GetAllProfilesAsync();
    }
}
