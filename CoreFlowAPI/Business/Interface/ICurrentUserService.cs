using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Interface
{
    public interface ICurrentUserService
    {
        Task<UserDTO?> UpsertCurrentUserAsync();
    }
}
