using AutoMapper;
using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;
using System.Security.Claims;

namespace CoreFlowAPI.Business.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO?> UpsertCurrentUserAsync()
        {
            var claims = _httpContextAccessor.HttpContext?.User;
            var email = claims?.FindFirstValue("preferred_username")
                     ?? claims?.FindFirstValue(ClaimTypes.Email);

            if (string.IsNullOrEmpty(email))
                return null;

            var name = claims?.FindFirstValue("name") ?? email;
            var user = await _userRepository.GetByEmailAsync(email);

            if (user is not null)
            {
                if (user.Name != name)
                {
                    await _userRepository.UpdateNameAsync(user.Id, name);
                    user.Name = name;
                }

                return _mapper.Map<UserDTO>(user);
            }

            var newUserId = await _userRepository.CreateAsync(new User
            {
                Name = name,
                Email = email,
                RoleId = 2
            });

            var newUser = await _userRepository.GetByIdAsync(newUserId);
            return _mapper.Map<UserDTO>(newUser);
        }
    }
}
