using AutoMapper;
using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.DTOs.Email; 
using CoreFlowSharedLibrary.Enums;
using CoreFlowSharedLibrary.Models;
using CoreFlowSharedLibrary.Services;

namespace CoreFlowAPI.Business.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidationService _validation;
        private readonly IEmailIntegrationService _emailService; 
        private readonly ILogger<CaseService> _logger; 

        public CaseService(
            ICaseRepository repository,
            IMapper mapper,
            IValidationService validation,
            IEmailIntegrationService emailService,
            ILogger<CaseService> logger)
        {
            _repo = repository;
            _mapper = mapper;
            _validation = validation;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> CreateAsync(CaseDTO obj)
        {
            
            await _validation.ValidateAndThrowAsync(obj);


            var model = _mapper.Map<Case>(obj);
            var employeeModel = _mapper.Map<Employee>(obj.Employee);
            var accountsModel = new List<Account>();
            foreach (var account in obj.Employee.Accounts)
            {
                accountsModel.Add(_mapper.Map<Account>(account));
            }


            var createdId = await _repo.CreateAsync(model, employeeModel, accountsModel);

            // 4. Skicka email i bakgrunden om allt gick bra
            if (createdId > 0)
            {
                await SendEmailInternalAsync(obj, createdId);
            }

            return createdId;
        }


        private async Task SendEmailInternalAsync(CaseDTO caseDto, int caseId)
        {
            try
            {
                var emp = caseDto.Employee;
                if (emp == null) return;

                var systems = emp.Accounts?
                    .Select(a => a.SystemAccessId.ToString())
                    .ToList() ?? new List<string>();

                if (caseDto.Type == TypeOfCase.Onboarding)
                {
                    await _emailService.SendOnboardingEmailAsync(new OnboardingEmailDto
                    {
                        CaseId = null,
                        FirstName = emp.FirstName ?? "",
                        LastName = emp.LastName ?? "",
                        PersonalNumber = emp.PersonalId?.Replace("-", "") ?? "",
                        Department = emp.Department ?? "",
                        Company = emp.Company.ToString(),
                        MobileNumber = emp.PhoneNumber ?? "",
                        EmploymentDate = emp.DateOfEmployment,
                        JobTitle = emp.Title.ToString(),
                        StartDate = emp.StartDate,
                        SelectedSystems = systems,
                        RequestedBy = "system@finansia.se"
                    });
                }
                else if (caseDto.Type == TypeOfCase.Offboarding)
                {
                    await _emailService.SendOffboardingEmailAsync(new OffboardingEmailDto
                    {
                        CaseId = null,
                        FirstName = emp.FirstName ?? "",
                        LastName = emp.LastName ?? "",
                        PersonalNumber = emp.PersonalId?.Replace("-", "") ?? "",
                        Department = emp.Department ?? "",
                        Company = emp.Company.ToString(),
                        MobileNumber = emp.PhoneNumber ?? "",
                        EmploymentDate = emp.DateOfEmployment,
                        JobTitle = emp.Title.ToString(),
                        StartDate = emp.StartDate,
                        SelectedSystems = systems,           
                        RequestedBy = "system@finansia.se" //hårdkoda en default-adress för nu 
                    });
                }

                _logger.LogInformation("Email skickat framgångsrikt för Case {CaseId} via CaseService", caseId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Misslyckades att skicka email för Case {CaseId} i CaseService", caseId);
            }
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