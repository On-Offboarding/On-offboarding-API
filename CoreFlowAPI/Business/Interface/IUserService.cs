using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(UserDTO user);
    }
}
