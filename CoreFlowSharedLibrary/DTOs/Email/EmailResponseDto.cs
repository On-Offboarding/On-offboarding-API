using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreFlowSharedLibrary.DTOs.Email;

public class EmailResponseDto
{

    public bool Success { get; set; }


    public string Message { get; set; } = string.Empty;

    /// Azure Message ID (för spårning i Azure Communication Services)
    public string? MessageId { get; set; }


    public DateTime SentAt { get; set; }


    public string? RecipientEmail { get; set; }


    public string? ErrorMessage { get; set; }
}
