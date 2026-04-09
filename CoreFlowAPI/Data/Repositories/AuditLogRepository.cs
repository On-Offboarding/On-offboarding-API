using AutoMapper;
using CoreFlowAPI.Data.Context;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Dapper;
namespace CoreFlowAPI.Data.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly IDbContext _dbContext;
        public AuditLogRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<int> CreateAsync(AuditLog log)
        {
            using var connection = _dbContext.CreateConnection();

            var sql = @"
             INSERT INTO dbo.AuditLog (Action, CaseId, TimeStamp)
             OUTPUT INSERTED.Id
             VALUES (@Action, @CaseId, @TimeStamp);
             ";

            return await connection.QuerySingleAsync<int>(sql, new
            {
                Action = log.Action,
                CaseId = log.CaseId,
                TimeStamp = log.TimeStamp
            });
        }

        public async Task<IEnumerable<AuditLog>> GetAllAsync()
        {

            using var connection = _dbContext.CreateConnection();
            var models = await connection.QueryAsync<AuditLog>(
                "select top 100 Id, Action, CaseId, TimeStamp from AuditLog order by TimeStamp desc");
            return models;
        }
    }
}
