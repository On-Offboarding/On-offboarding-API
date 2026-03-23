using CoreFlowSharedLibrary.DTOs.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.Services;

public interface IEmailIntegrationService
{

    // Skickar onboarding email till CTO via EmailApi
    Task<EmailResponseDto> SendOnboardingEmailAsync(OnboardingEmailDto dto);

    /// Skickar offboarding email till CTO via EmailApi

    Task<EmailResponseDto> SendOffboardingEmailAsync(OffboardingEmailDto dto);
}
