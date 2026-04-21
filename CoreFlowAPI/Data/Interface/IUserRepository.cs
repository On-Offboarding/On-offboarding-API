using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Data.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<int> CreateAsync(User user);
        Task UpdateNameAsync(int userId, string name);
        Task<bool> UpdateRoleAsync(int userId, int roleId);
    }
}
