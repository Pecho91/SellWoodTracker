using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker.GlobalConfig;
using SellWoodTracker.Common.Model;
using System.Data;
using System.Runtime.CompilerServices;
using Dapper;
using System.Data.Common;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using SellWoodTracker.DataAccess.SqlConnectionFactory;
using SellWoodTracker.DataAccess.SqlConnectionExecutor;
using System.Reflection;

namespace SellWoodTracker.DataAccess.SqlDataRepository
{
    public class SqlPersonRepository : ISqlPersonRepository
    {
        private readonly ISqlConnectionExecutor _sqlConnectionExecutor;
        private readonly ISqlDynamicParametersBuilder _sqlDynamicParametersBuilder;

        public ISqlConnectionExecutor SqlConnectionExecutor => _sqlConnectionExecutor;
        public ISqlDynamicParametersBuilder SqlDynamicParametersBuilder => _sqlDynamicParametersBuilder;


        public SqlPersonRepository(ISqlConnectionExecutor sqlConnectionExecutor, ISqlDynamicParametersBuilder sqlDynamicParametersBuilder)
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

        public PersonModel? GetPersonById(int personId)
        {
            return _sqlConnectionExecutor.Execute(connection =>
            {
                return connection.QueryFirstOrDefault<PersonModel>("dbo.spRequestedPeople_GetById",
                    new { Id = personId }, commandType: CommandType.StoredProcedure);
            });
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

        public void MoveRequestedPersonToCompleted(int personId)
        {
            _sqlConnectionExecutor.Execute(connection =>
            {
                var person = GetPersonById(personId);

                if (person != null)
                {
                    connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);

                    var parameters = _sqlDynamicParametersBuilder.GetPersonDynamicParameters(person);
                    connection.Execute("dbo.spCompletedPeople_Insert", parameters, commandType: CommandType.StoredProcedure);
                }
            });
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

        public decimal GetTotalGrossIncomeFromCompleted()
        {
            return _sqlConnectionExecutor.Execute(connection =>
            {
                return connection.Query<decimal>("dbo.spCompletedPeople_GetTotalGrossIncome").FirstOrDefault();
            });
        }

        public decimal GetTotalMetricAmountFromCompleted()
        {
            return _sqlConnectionExecutor.Execute(connection =>
             {
                 return connection.Query<decimal>("dbo.spCompletedPeople_GetTotalMetricAmount").FirstOrDefault();
             });
        }
    }
}
