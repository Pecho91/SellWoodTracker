using Dapper;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlConnectionExecutor;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataRepositories
{
    public class SqlPeopleListRetriever : ISqlPeopleListRetriever
    {
        private readonly ISqlConnectionExecutor _sqlConnectionExecutor;

        public SqlPeopleListRetriever(ISqlConnectionExecutor sqlConnectionExecutor)
        {
            _sqlConnectionExecutor = sqlConnectionExecutor ?? throw new ArgumentNullException(nameof(sqlConnectionExecutor));  
        }

        public List<PersonModel> GetRequestedPeople_All()
        {
            return _sqlConnectionExecutor.Execute(connection =>
            {
                return connection.Query<PersonModel>("dbo.spRequestedPeople_GetAll").ToList();
            });
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
