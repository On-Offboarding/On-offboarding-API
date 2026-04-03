using AutoMapper;
using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowAPI.Data.Repositories;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;

namespace CoreFlowAPI.Business.Services
{
    public class CaseService : ICaseService
    {
        ICaseRepository _repo;
        IMapper _mapper;
        IValidationService _validation;

        public CaseService(ICaseRepository repository, IMapper mapper, IValidationService validation)
        {
            _repo = repository;
            _mapper = mapper;
            _validation = validation;
        }
        public async Task<int> CreateAsync(CreateCaseDTO dto)
        {
            await _validation.ValidateAndThrowAsync(dto);
            var model = _mapper.Map<Case>(dto);
            var employeeModel = _mapper.Map<Employee>(dto.Employee);
            var accountsModel = new List<Account>();
            if (dto.Employee.Accounts != null && dto.Employee.Accounts.Count > 0)
            {
                foreach (var account in dto.Employee.Accounts)
                {
                    accountsModel.Add(_mapper.Map<Account>(account));
                } 
            }

            return await _repo.CreateAsync(model,employeeModel,accountsModel);
        }

        
        public async Task<bool> UpdateAsync(CaseDTO dto)
        {
            await _validation.ValidateAndThrowAsync(dto);
            var existing = await _repo.GetByIdAsync(dto.Id);
            if (existing == null)
                return false;

            var caseModel = _mapper.Map<Case>(dto);
            var employeeModel = _mapper.Map<Employee>(dto.Employee);
            var accountsModel = new List<Account>();

            employeeModel.Id = existing.Employee.Id;
            if (dto.Employee.Accounts != null && dto.Employee.Accounts.Count > 0)
            {
                foreach (var account in dto.Employee.Accounts)
                {
                    accountsModel.Add(_mapper.Map<Account>(account));
                }
            }

            return await _repo.UpdateAsync(caseModel, employeeModel, accountsModel);

        }


        public Task<IEnumerable<CaseDTO>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }
        public Task<IEnumerable<CaseDTO>> GetAllAsync(StatusOfCase status)
        {
            return _repo.GetAllAsync(status);
        }

        public async Task<CaseDTO?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                return null;
            return _mapper.Map<CaseDTO>(entity);
        }
    }
}
