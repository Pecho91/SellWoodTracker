using Dapper;
using SellWoodTracker.Common.Model;
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
    public class SqlPersonRetriever : ISqlPersonRetriever
    {
        private readonly ISqlConnectionExecutor _sqlConnectionExecutor;

        public SqlPersonRetriever(ISqlConnectionExecutor sqlConnectionExecutor)
        {
            _sqlConnectionExecutor = sqlConnectionExecutor ?? throw new ArgumentNullException(nameof(sqlConnectionExecutor));
        }
     

        public PersonModel? GetPersonById(int personId)
        {
            return _sqlConnectionExecutor.Execute(connection =>
            {
                return connection.QueryFirstOrDefault<PersonModel>("dbo.spRequestedPeople_GetById",
                    new { Id = personId }, commandType: CommandType.StoredProcedure);
            });
        }
    }
}
