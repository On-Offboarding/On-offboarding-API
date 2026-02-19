using AutoMapper;
using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Services
{
    public class SystemAccessService : ISystemAccessService
    {
        ISystemAccessRepository _repo;
        IMapper _mapper;
        public SystemAccessService(ISystemAccessRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repository;
        }
        public async Task<IEnumerable<SystemAccessDTO>> GetAllAsync()
        {
            var models = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<SystemAccessDTO>>(models);
        }

        public Task<IEnumerable<ProfileSystemAccessDTO>> GetAllProfilesAsync()
        {
            return _repo.GetAllProfilesAsync();
        }
    }
}
