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
        public async Task<int> CreateAsync(UserDTO user)
        {
            var model = _mapper.Map<User>(user);
            using var connection = _dbContext.CreateConnection();

            var sql = @"
             INSERT INTO dbo.Users (Name, Email, RoleId)
             OUTPUT INSERTED.Id
             VALUES (@Name, @Email, @RoleId);
             ";

            return await connection.QuerySingleAsync<int>(sql, new
            {
                Name = model.Name,
                Email = model.Email,
                RoleId = model.RoleId
            });
            
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            using var connection = _dbContext.CreateConnection();
            var models = await connection.QueryAsync<User>(
                "select Id, Name, Email, RoleId from users");
            return _mapper.Map<List<UserDTO>>(models);
          
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.CreateConnection();
            var model = await connection.QueryFirstOrDefaultAsync<UserDTO>(
                "select Id, Name, Email, RoleId from users where Id = @Id", new { Id = id });
            return _mapper.Map<UserDTO>(model);
        }
    }
}
