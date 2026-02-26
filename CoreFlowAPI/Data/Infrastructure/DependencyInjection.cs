using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Business.Services;
using CoreFlowAPI.Business.Validation;
using CoreFlowAPI.Data.Context;
using CoreFlowAPI.Data.Interface;
using CoreFlowAPI.Data.Mapping.TypeConverters;
using CoreFlowAPI.Data.Repositories;
using CoreFlowSharedLibrary.DTOs;
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
            
            return services;

        }
        public static IServiceCollection AddValidators(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddScoped<IValidationService, ValidationService>();
            services.AddValidatorsFromAssemblyContaining<UserDTO>();
            services.AddValidatorsFromAssemblyContaining<CaseDTO>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISystemAccessService, SystemAccessService>();
            services.AddScoped<IntToTypeOfCaseConverter>();
            services.AddScoped<IntToStatusOfCaseConverter>();
            services.AddScoped<IntToStatusOfAccountConverter>();
            services.AddScoped<StringToTitleOfEmployeeConverter>();

            return services;
        }
    }
}
