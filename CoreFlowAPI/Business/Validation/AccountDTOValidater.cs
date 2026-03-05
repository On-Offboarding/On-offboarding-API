using CoreFlowAPI.Data.Context;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using Dapper;
using FluentValidation;

namespace CoreFlowAPI.Business.Validation
{
    public class AccountDTOValidater : AbstractValidator<AccountDTO>
    {

        IDbContext _db;
        public AccountDTOValidater(IDbContext db) 
        {
            _db = db;
            RuleFor(x => x.SystemAccessId)
                .NotEqual(0)
                .MustAsync(SystemAccessIdExists);
        }

        private async Task<bool> SystemAccessIdExists(int id, CancellationToken token)
        {
            var connection = _db.CreateConnection();
            const string sql = """
                select 1 from SystemAccesses where Id = @Id
                """;
            var result = await connection.ExecuteScalarAsync<int?>(
                new CommandDefinition(sql, new { Id = id }, cancellationToken: token));
            return result.HasValue;


        }
    }
}
