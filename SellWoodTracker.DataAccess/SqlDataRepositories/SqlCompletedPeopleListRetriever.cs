using Dapper;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataRepositories
{
    public class SqlCompletedPeopleListRetriever : ISqlCompletedPeopleListRetriever
    {
        private readonly ISqlConnectionExecutor _sqlConnectionExecutor;

        public SqlCompletedPeopleListRetriever(ISqlConnectionExecutor sqlConnectionExecutor) 
        {
            _sqlConnectionExecutor = sqlConnectionExecutor ?? throw new ArgumentNullException(nameof(sqlConnectionExecutor));
        }

        public List<PersonModel> GetCompletedPeople_All()
        {
            return _sqlConnectionExecutor.Execute(connection =>
            {
                return connection.Query<PersonModel>("dbo.spCompletedPeople_GetAll").ToList();
            });
        }
    }
}
