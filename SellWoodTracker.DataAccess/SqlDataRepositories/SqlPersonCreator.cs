using Dapper;
using SellWoodTracker.Common.Model;
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
    public class SqlPersonCreator : ISqlPersonCreator
    {
        private readonly ISqlConnectionExecutor _sqlConnectionExecutor;
        private readonly ISqlDynamicParametersBuilder _sqlDynamicParametersBuilder;

        //public ISqlConnectionExecutor SqlConnectionExecutor => _sqlConnectionExecutor;
        //public ISqlDynamicParametersBuilder SqlDynamicParametersBuilder => _sqlDynamicParametersBuilder;

        public SqlPersonCreator(ISqlConnectionExecutor sqlConnectionExecutor, ISqlDynamicParametersBuilder sqlDynamicParametersBuilder)
        {
            _sqlConnectionExecutor = sqlConnectionExecutor ?? throw new ArgumentNullException(nameof(sqlConnectionExecutor));
            _sqlDynamicParametersBuilder = sqlDynamicParametersBuilder ?? throw new ArgumentNullException(nameof(sqlDynamicParametersBuilder));
        }

        public void CreatePerson(PersonModel model)
        {
            var p = _sqlDynamicParametersBuilder.GetPersonDynamicParameters(model);
            _sqlConnectionExecutor.Execute(connection =>
            {
                connection.Execute("dbo.spRequestedPeople_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            });
        }
    }
}