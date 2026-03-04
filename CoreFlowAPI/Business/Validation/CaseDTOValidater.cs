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
            RuleFor(x => x.Type)
                .IsInEnum()
                .Must(x => x == TypeOfCase.Onboarding
                || x == TypeOfCase.Offboarding);
            RuleFor(x => x.Status)
                .IsInEnum()
                .NotEqual(StatusOfCase.None);
            RuleFor(x => x.Employee)
                .SetValidator(validator);
            RuleFor(x => x.CreatedByUser)
                .MustAsync(UserIdExists)
                .WithMessage("Invalid UserId");

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
