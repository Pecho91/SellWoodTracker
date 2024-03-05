using Dapper;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataRepositories
{
    public class SqlPersonDeleter : ISqlPersonDeleter
    {
        private readonly ISqlConnectionExecutor _sqlConnectionExecutor;

        public SqlPersonDeleter(ISqlConnectionExecutor sqlConnectionExecutor)
        {
            _sqlConnectionExecutor = sqlConnectionExecutor ?? throw new ArgumentNullException(nameof(sqlConnectionExecutor));
        }

        public void DeletePersonFromRequested(int personId)
        {
            _sqlConnectionExecutor.Execute(connection =>
            {
                connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);
            });
        }

        public void DeletePersonFromCompleted(int personId)
        {
            _sqlConnectionExecutor.Execute(connection =>
            {
                connection.Execute("dbo.spCompletedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
