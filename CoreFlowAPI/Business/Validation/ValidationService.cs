using CoreFlowAPI.Business.Interface;
using FluentValidation;

namespace CoreFlowAPI.Business.Validation
{
    public sealed class ValidationService : IValidationService
    {
        private readonly IServiceProvider _provider;
        public ValidationService(IServiceProvider sp)
        {
            _provider = sp;
        }
        public async Task ValidateAndThrowAsync<T>(T model, CancellationToken ct = default)
        {
            var validator = _provider.GetService<IValidator<T>>();

            if (validator == null) 
                return; 

            await validator.ValidateAndThrowAsync(model, ct);
            
        }
    }
}
