using AutoMapper;
using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Business.Services
{
    public class UserService : IUserService
    {
        IUserRepository _repo;
        IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(UserDTO user)
        {
            var model = _mapper.Map<User>(user);
            return await _repo.CreateAsync(model);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
