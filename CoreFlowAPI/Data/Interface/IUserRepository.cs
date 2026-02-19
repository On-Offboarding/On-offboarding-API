using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<int> CreateAsync(User user);
    }
}
