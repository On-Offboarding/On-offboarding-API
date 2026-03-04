using AutoMapper;
using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Business.Services
{
    public class CaseService : ICaseService
    {
        ICaseRepository _repo;
        IMapper _mapper;

        public CaseService(ICaseRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(CaseDTO @case)
        {
            var model = _mapper.Map<Case>(@case);
            var employeeModel = _mapper.Map<Employee>(@case.Employee);
            var accountsModel = new List<Account>();
            foreach (var account in @case.Employee.Accounts)
            {
                accountsModel.Add(_mapper.Map<Account>(account));
            }

            return await _repo.CreateAsync(model,employeeModel,accountsModel);
        }

        public Task<IEnumerable<CaseDTO>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }
        public Task<IEnumerable<CaseDTO>> GetAllAsync(StatusOfCase status)
        {
            return _repo.GetAllAsync(status);
        }

        public Task<CaseDTO?> GetByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }
    }
}
