using CoreFlowSharedLibrary.DTOs;
using FluentValidation;
using CoreFlowSharedLibrary.Enums;
using CoreFlowAPI.Data.Interface;
using Dapper;

namespace CoreFlowAPI.Business.Validation
{
    public class CaseDTOValidater : AbstractValidator<CaseDTO>
    {
        private readonly IDbContext dbContext;

        public CaseDTOValidater(IValidator<EmployeeDTO> validator, IDbContext db)
        {
            dbContext = db;

            // Grundläggande regler för Case
            RuleFor(x => x.Type).IsInEnum();
            RuleFor(x => x.Status).IsInEnum().NotEqual(StatusOfCase.None);
            RuleFor(x => x.CreatedByUser).MustAsync(UserIdExists);

            // den vanliga Employee-valideringen (namn, telefon etc.)
            RuleFor(x => x.Employee).SetValidator(validator);

            // --- SPECIFIKA REGLER FÖR ONBOARDING ---
            // Dessa körs BARA om Type är Onboarding (1)
            When(x => x.Type == TypeOfCase.Onboarding, () => {
                RuleFor(x => x.Employee.StartDate)
                    .NotEmpty();
                    //.GreaterThanOrEqualTo(DateTime.Today)
                    //.WithMessage("Vid onboarding måste startdatum vara idag eller framåt i tiden.");

                RuleFor(x => x.Employee.DateOfEmployment)
                    .NotEmpty()
                    .LessThanOrEqualTo(x => x.Employee.StartDate)
                    .WithMessage("Anställningsdatum kan inte vara efter startdatum.");
            });

            // --- SPECIFIKA REGLER FÖR OFFBOARDING ---
            
            When(x => x.Type == TypeOfCase.Offboarding, () => {
                RuleFor(x => x.Employee.EndDate)
                    .NotEmpty()
                    .WithMessage("Slutdatum krävs vid offboarding.");
            });
        }
        private async Task<bool> UserIdExists(int userId,CancellationToken token)
        {
           var connection = dbContext.CreateConnection();
            const string sql = """
                select 1 from Users where Id = @Id
                """;
            var result = await connection.ExecuteScalarAsync<int?>(
                new CommandDefinition(sql, new { Id = userId },cancellationToken:token));
            return result.HasValue;


        }


    }

}
