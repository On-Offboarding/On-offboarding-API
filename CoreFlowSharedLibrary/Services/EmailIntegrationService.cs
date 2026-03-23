using CoreFlowSharedLibrary.DTOs.Email;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
namespace CoreFlowSharedLibrary.Services;

/// Service som anropar EmailApi microservice för att skicka emails
/// EmailApi är ansvarig för att generera HTML och skicka via Azure Communication Services
public class EmailIntegrationService : IEmailIntegrationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<EmailIntegrationService> _logger;

    public EmailIntegrationService(
        IHttpClientFactory httpClientFactory,
        ILogger<EmailIntegrationService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// Skickar onboarding email till CTO via EmailApi

    public async Task<EmailResponseDto> SendOnboardingEmailAsync(OnboardingEmailDto dto)
    {
        try
        {
            _logger.LogInformation(
                "Anropar EmailApi för onboarding: {FirstName} {LastName}, Case: {CaseId}",
                dto.FirstName,
                dto.LastName,
                dto.CaseId);

            var client = _httpClientFactory.CreateClient("EmailApi");

            var response = await client.PostAsJsonAsync(
                "/api/email/send-onboarding", 
                dto);                           


            if (response.IsSuccessStatusCode)
            {

                var result = await response.Content
                    .ReadFromJsonAsync<EmailResponseDto>();

                if (result != null)
                {
                    _logger.LogInformation(
                        "Onboarding email skickat via EmailApi. Message ID: {MessageId}, Success: {Success}",
                        result.MessageId,
                        result.Success);

                    return result;
                }


                _logger.LogWarning("EmailApi returnerade success men ingen response body");
                return new EmailResponseDto
                {
                    Success = false,
                    Message = "Inget response från EmailApi",
                    ErrorMessage = "No response body from EmailApi",
                    SentAt = DateTime.UtcNow
                };
            }

            var errorBody = await response.Content.ReadAsStringAsync();
            _logger.LogError(
                "EmailApi returnerade fel: {StatusCode} - Body: {Body}",
                response.StatusCode,
                errorBody);

            return new EmailResponseDto
            {
                Success = false,
                Message = $"EmailApi error: {response.StatusCode}",
                ErrorMessage = response.ReasonPhrase,
                SentAt = DateTime.UtcNow
            };
        }
        catch (HttpRequestException ex)
        {

            _logger.LogError(ex,
                "HTTP-fel vid anrop till EmailApi. Är EmailApi igång på rätt port?");

            return new EmailResponseDto
            {
                Success = false,
                Message = "Kunde inte nå EmailApi",
                ErrorMessage = ex.Message,
                SentAt = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {

            _logger.LogError(ex, "Oväntat fel vid anrop till EmailApi");

            return new EmailResponseDto
            {
                Success = false,
                Message = "Internt fel vid email-skickning",
                ErrorMessage = ex.Message,
                SentAt = DateTime.UtcNow
            };
        }
    }

    /// Skickar offboarding email till CTO via EmailApi

    public async Task<EmailResponseDto> SendOffboardingEmailAsync(OffboardingEmailDto dto)
    {
        try
        {
            _logger.LogInformation(
                "Anropar EmailApi för offboarding: {FirstName} {LastName}, Case: {CaseId}",
                dto.FirstName,
                dto.LastName,
                dto.CaseId);

            var client = _httpClientFactory.CreateClient("EmailApi");

            var response = await client.PostAsJsonAsync(
                "/api/email/send-offboarding",  
                dto);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content
                    .ReadFromJsonAsync<EmailResponseDto>();

                if (result != null)
                {
                    _logger.LogInformation(
                        "Offboarding email skickat via EmailApi. Message ID: {MessageId}",
                        result.MessageId);

                    return result;
                }

                _logger.LogWarning("EmailApi returnerade success men ingen response body");
                return new EmailResponseDto
                {
                    Success = false,
                    Message = "Inget response från EmailApi",
                    SentAt = DateTime.UtcNow
                };
            }

            var errorBody = await response.Content.ReadAsStringAsync();
            _logger.LogError(
                "EmailApi returnerade fel: {StatusCode} - Body: {Body}",
                response.StatusCode,
                errorBody);

            return new EmailResponseDto
            {
                Success = false,
                Message = $"EmailApi error: {response.StatusCode}",
                ErrorMessage = response.ReasonPhrase,
                SentAt = DateTime.UtcNow
            };
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP-fel vid anrop till EmailApi för offboarding");

            return new EmailResponseDto
            {
                Success = false,
                Message = "Kunde inte nå EmailApi",
                ErrorMessage = ex.Message,
                SentAt = DateTime.UtcNow
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Oväntat fel vid offboarding email");

            return new EmailResponseDto
            {
                Success = false,
                Message = "Internt fel",
                ErrorMessage = ex.Message,
                SentAt = DateTime.UtcNow
            };
        }
    }
}
