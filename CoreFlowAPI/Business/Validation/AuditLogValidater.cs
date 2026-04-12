using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using FluentValidation;

namespace CoreFlowAPI.Business.Validation
{
    public class AuditLogValidater : AbstractValidator<AuditLogDTO>
    {
        public AuditLogValidater()
        {
            RuleFor(x => x.Action).IsInEnum().NotEqual(AuditAction.Unknown);
        }
    }
}
