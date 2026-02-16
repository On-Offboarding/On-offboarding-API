using System.Data;
using CoreFlowAPI.Data.Interface;
using Microsoft.Data.SqlClient;

namespace CoreFlowAPI.Data.Context
{
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _configuration;

        public DbContext(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

             
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("AppDb"));
        }
    }
}
