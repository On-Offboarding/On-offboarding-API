using AutoMapper;
using CoreFlowAPI.Data.Interface;
using CoreFlowSharedLibrary.DTOs;
using CoreFlowSharedLibrary.Models;
using Dapper;

namespace CoreFlowAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserRepository(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(User user)
        {
            using var connection = _dbContext.CreateConnection();

            var sql = @"
             INSERT INTO dbo.Users (Name, Email, RoleId)
             OUTPUT INSERTED.Id
             VALUES (@Name, @Email, @RoleId);
             ";

            return await connection.QuerySingleAsync<int>(sql, new
            {
                Name = user.Name,
                Email = user.Email,
                RoleId = user.RoleId
            });
            
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            var models = await connection.QueryAsync<User>(
                "select Id, Name, Email, RoleId from users");
            return models;
          
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            var model = await connection.QueryFirstOrDefaultAsync<User>(
                "select Id, Name, Email, RoleId from users where Id = @Id", new { Id = id });
            return model;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "select Id, Name, Email, RoleId from users where Email = @Email", new { Email = email });
        }

        public async Task UpdateNameAsync(int userId, string name)
        {
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(
                "update dbo.Users set Name = @Name where Id = @Id",
                new { Name = name, Id = userId });
        }

        public async Task<bool> UpdateRoleAsync(int userId, int roleId)
        {
            using var connection = _dbContext.CreateConnection();
            var rows = await connection.ExecuteAsync(
                "update dbo.Users set RoleId = @RoleId where Id = @Id",
                new { RoleId = roleId, Id = userId });
            return rows > 0;
        }
    }
}
