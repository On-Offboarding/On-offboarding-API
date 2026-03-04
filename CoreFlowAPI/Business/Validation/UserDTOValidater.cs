using CoreFlowSharedLibrary.DTOs;
using FluentValidation;

namespace CoreFlowAPI.Business.Validation
{
    public class UserDTOValidater : AbstractValidator<UserDTO>
    {
        public UserDTOValidater()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .InclusiveBetween(1, 2);
        }
    }
}
