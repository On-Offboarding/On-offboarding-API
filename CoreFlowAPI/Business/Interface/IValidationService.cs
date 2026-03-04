namespace CoreFlowAPI.Business.Interface
{
    public interface IValidationService
    {
        Task ValidateAndThrowAsync<T>(T model, CancellationToken ct = default);
    }
}
