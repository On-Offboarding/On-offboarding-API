using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using FluentValidation;
using Personnummer;

namespace CoreFlowAPI.Business.Validation
{
    public class EmployeeDTOValidater : AbstractValidator<EmployeeDTO>
    {
        public EmployeeDTOValidater(IValidator<AccountDTO> validator) 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Title) 
                .MaximumLength(25);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^(?:\+46|0)\s?7[02369]\d{7}$");
            RuleFor(x => x.PersonalId)
                .NotEmpty()
                .Length(13)
                .Matches(@"^\d{8}-\d{4}$")
                .WithMessage("Bad Format (YYYYMMDD-XXXX)")
                .Must(BeValidPersonalNumber);
            RuleFor(x => x.Company)
                .IsInEnum()
                .NotEqual(CompanyOfEmployee.Unknown);
            RuleFor(x => x.Department)
                .NotEmpty()
                .MaximumLength(25);
            RuleFor(x => x.StartDate)
                .NotEmpty();
            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .When(x => x.EndDate.HasValue);
            RuleFor(x => x.DateOfEmployment)
                .NotEmpty()
                .LessThanOrEqualTo(x => x.StartDate);
            RuleForEach(x => x.Accounts)
                .SetValidator(validator);

        }

        private bool BeValidPersonalNumber(string personalId)
        {
            return Personnummer.Personnummer.Valid(personalId);
        }
    }
}
