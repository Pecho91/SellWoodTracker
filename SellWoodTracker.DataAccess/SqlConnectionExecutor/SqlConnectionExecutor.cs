using SellWoodTracker.DataAccess.SqlConnectionFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlConnectionExecutor
{
    public class SqlConnectionExecutor : ISqlConnectionExecutor
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public SqlConnectionExecutor(ISqlConnectionFactory sqlConnectionFactory) 
        {
            _sqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
        }
        public void Execute(Action<IDbConnection> action)
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                connection.Open();
                action(connection);
            }
        }

        public T Execute<T>(Func<IDbConnection, T> executeFunction)
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                connection.Open();
                return executeFunction(connection);
            }
        }
    }
}
