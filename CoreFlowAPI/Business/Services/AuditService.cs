using AutoMapper;
using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;
using DocumentFormat.OpenXml.Office.CustomUI;

namespace CoreFlowAPI.Business.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repo;
        private readonly ICaseRepository _caseRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IValidationService _validation;
        public AuditLogService(IAuditLogRepository audit, IMapper maps, IValidationService validation, ICaseRepository caseRepo, IUserRepository userRepository)
        {
            _repo = audit;
            _mapper = maps;
            _validation = validation;
            _caseRepo = caseRepo;
            _userRepo = userRepository;
        }
        private async Task<int> CreateAsync(AuditLogDTO logDTO)
        {
            // await _validation.ValidateAndThrowAsync(logDTO);
            var log = _mapper.Map<AuditLog>(logDTO);
            return await _repo.CreateAsync(log);
        }

        public async Task<int> CreateCaseAsync(CaseDTO dTO)
        {
            var log = new AuditLogDTO
            {
                Action = AuditAction.CaseCreated,
                CaseId = dTO.Id,
                TimeStamp = DateTime.Now
            };
            return await CreateAsync(log);
        }

        public async Task<int> UpdateCaseAsync(CaseDTO dTO)
        {
            var action = dTO.Status == StatusOfCase.Completed ? AuditAction.CaseCompleted : AuditAction.CaseUpdated;
            var log = new AuditLogDTO
            {
                Action = action,
                CaseId = dTO.Id,
                TimeStamp = DateTime.Now
            };
            return await CreateAsync(log);
        }
        

        public async Task<List<AuditLogViewDTO>> GetAllAuditsAsync()
        {
            var logs = await _repo.GetAllAsync();
            var dtos = _mapper.Map<List<AuditLogDTO>>(logs);
            foreach (var item in dtos)
            {
                item.Case = await _caseRepo.GetByIdAsync(item.CaseId ?? 0);
                if (item.Case != null)
                {
                    item.User = _mapper.Map<UserDTO>(await _userRepo.GetByIdAsync(item.Case.CreatedByUser));
                }
            }
            return _mapper.Map<List<AuditLogViewDTO>>(dtos);

        }
    }
}
