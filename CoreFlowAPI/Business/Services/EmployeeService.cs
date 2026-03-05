using AutoMapper;
using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;

namespace CoreFlowAPI.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _repo;
        IMapper _mapper;
        public EmployeeService(IEmployeeRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public Task<IEnumerable<EmployeeDTO>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }

        public Task<EmployeeDTO?> GetByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }
    }
}
