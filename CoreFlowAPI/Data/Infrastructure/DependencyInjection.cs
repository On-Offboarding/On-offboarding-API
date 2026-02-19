using CoreFlowAPI.Business.Interface;
using CoreFlowAPI.Business.Services;
using CoreFlowAPI.Data.Context;
using CoreFlowAPI.Data.Interface;
using CoreFlowAPI.Data.Mapping.TypeConverters;
using CoreFlowAPI.Data.Repositories;

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
