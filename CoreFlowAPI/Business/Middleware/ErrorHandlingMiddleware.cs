using CoreFlowSharedLibrary.Domain;
using FluentValidation;

namespace CoreFlowAPI.Business.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation failed");
                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                var response = new ErrorResponse
                {
                    Message = "Validation failed",
                    StatusCode = StatusCodes.Status400BadRequest,
                    TraceId = context.TraceIdentifier,
                    Errors = errors
                };

                await HandleException(context, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                var response = new ErrorResponse
                {
                    Message = ex.Message + " | " + ex.InnerException?.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    TraceId = context.TraceIdentifier
                };
                context.Response.ContentType = "application/json";
                await HandleException(context, response);
            }
        }

        private static async Task HandleException(HttpContext context, ErrorResponse error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
