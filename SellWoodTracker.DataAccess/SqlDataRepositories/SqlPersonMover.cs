using Dapper;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataRepositories
{
    public class SqlPersonMover : ISqlPersonMover
    {
        private readonly ISqlConnectionExecutor _sqlConnectionExecutor;
        private readonly ISqlDynamicParametersBuilder _sqlDynamicParametersBuilder;
        private readonly ISqlPersonRetriever _sqlPersonRetriever;

        public SqlPersonMover(ISqlConnectionExecutor sqlConnectionExecutor, ISqlDynamicParametersBuilder sqlDynamicParametersBuilder, ISqlPersonRetriever sqlPersonRetriever)
        {
            _sqlConnectionExecutor = sqlConnectionExecutor ?? throw new ArgumentNullException(nameof(sqlConnectionExecutor));   
            _sqlPersonRetriever = sqlPersonRetriever ?? throw new ArgumentNullException(nameof(sqlPersonRetriever));
            _sqlDynamicParametersBuilder = sqlDynamicParametersBuilder ?? throw new ArgumentNullException(nameof(sqlDynamicParametersBuilder));   
        }

        public void MoveRequestedPersonToCompleted(int personId)
        {
            _sqlConnectionExecutor.Execute(connection =>
            {
                var person = _sqlPersonRetriever.GetPersonById(personId);

                if (person != null)
                {
                    connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);

                    var parameters = _sqlDynamicParametersBuilder.GetPersonDynamicParameters(person);
                    connection.Execute("dbo.spCompletedPeople_Insert", parameters, commandType: CommandType.StoredProcedure);
                }
            });
        }
    }   
}
