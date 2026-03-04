using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Enums;
using FluentValidation;
using Personnummer;

namespace CoreFlowAPI.Business.Validation
{
    public class EmployeeDTOValidater : AbstractValidator<EmployeeDTO>
    {
        public EmployeeDTOValidater() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Title)
                 .NotEmpty()   
                .MaximumLength(25);
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\0\d{7,15}$")
                .WithMessage("Invalid Phonenumber");
            RuleFor(x => x.PersonalId)
                .NotEmpty()
                .Length(13)
                .Matches(@"^\d{8}-\d{4}$")
                .WithMessage("Bad Format (YYYYMMDD-XXXX)")
                .Must(BeValidPersonalNumber)
                .WithMessage("Invalid SSN");
            RuleFor(x => x.Company)
                .IsInEnum()
                .NotEqual(CompanyOfEmployee.Unknown);
            RuleFor(x => x.Department)
                .NotEmpty()
                .MaximumLength(25);

        }

        private bool BeValidPersonalNumber(string personalId)
        {
            return Personnummer.Personnummer.Valid(personalId);
        }
    }
}
