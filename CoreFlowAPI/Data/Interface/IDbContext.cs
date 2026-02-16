using System.Data;

namespace CoreFlowAPI.Data.Interface
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }
}
