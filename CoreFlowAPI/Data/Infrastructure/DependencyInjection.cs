using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Business.Services;
using CoreFlowAPI.Business.Validation;
using CoreFlowAPI.Data.Context;
using CoreFlowAPI.Data.Interface;
using CoreFlowAPI.Data.Mapping.MappingResolvers;
using CoreFlowAPI.Data.Mapping.TypeConverters;
using CoreFlowAPI.Data.Repositories;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Services;
using FluentValidation;

namespace CoreFlowAPI.Data.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IDbContext, DbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISystemAccessRepository, SystemAccessRepository>();
            services.AddScoped<ICaseRepository, CaseRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IPDFService, PDFService>();

            return services;

        }
        public static IServiceCollection AddValidators(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<IValidationService, ValidationService>();
            services.AddValidatorsFromAssemblyContaining<UserDTOValidater>();
            services.AddValidatorsFromAssemblyContaining<CaseDTOValidater>();
            services.AddValidatorsFromAssemblyContaining<EmployeeDTOValidater>();
            services.AddValidatorsFromAssemblyContaining<AccountDTOValidater>();
            services.AddValidatorsFromAssemblyContaining<UserDTO>();
            services.AddValidatorsFromAssemblyContaining<CaseDTO>();
            services.AddValidatorsFromAssemblyContaining<EmployeeDTO>();
            services.AddValidatorsFromAssemblyContaining<AccountDTO>();
            services.AddValidatorsFromAssemblyContaining<AuditLogValidater>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISystemAccessService, SystemAccessService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAuditLogService, AuditLogService>();
            services.AddScoped<IntToTypeOfCaseConverter>();
            services.AddScoped<IntToStatusOfCaseConverter>();
            services.AddScoped<IntToStatusOfAccountConverter>();
            services.AddScoped<BirthDatePartResolver>();
            services.AddScoped<PersonalIdLastDigitsResolver>();
            services.AddScoped<IEmailIntegrationService, EmailIntegrationService>();
            services.AddScoped<IExcelService, ExcelService>();

            return services;
        }
    }
}
