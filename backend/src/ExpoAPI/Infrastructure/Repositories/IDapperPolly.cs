using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ExpoAPI.Infrastructure.Repositories
{
    public interface IDapperPolly
    {
        Task<IEnumerable<T>> QueryAsyncWithRetry<T>(IDbConnection cnn, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}