using FluentValidation.Results;

namespace CoreFlowAPI.Business.Validation.Helpers
{
    public class ValidationErrorResponse
    {
        public string? Message { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }

        public static ValidationErrorResponse ToValidationErrorResponse(ValidationResult result)
        {
            return new ValidationErrorResponse
            {
                Message = "Validation failed",
                Errors = result.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    )
            };
        }
    }
}
