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
using SellWoodTracker.DataAccess.SqlDataRepository;
using System.Data.Common;
using SellWoodTracker.DataAccess.SqlDynamicParameters;

namespace SellWoodTracker.DataAccess.SqlDataAccess
{
    public class SqlPersonRepository : ISqlPersonRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ISqlDynamicParametersBuilder _sqlDynamicParametersBuilder;

        public ISqlConnectionFactory SqlConnectionFactory => _sqlConnectionFactory;
        public ISqlDynamicParametersBuilder SqlDynamicParametersBuilder => _sqlDynamicParametersBuilder;

        //public SqlPersonRepository(ISqlConnectionFactory sqlConnectionFactory)
        //{
        //    _sqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));       
        //}

        //public SqlPersonRepository(ISqlDynamicParametersBuilder sqlDynamicParametersBuilder)
        //{          
        //    _sqlDynamicParametersBuilder = sqlDynamicParametersBuilder ?? throw new ArgumentNullException(nameof(sqlDynamicParametersBuilder));
        //}

        public SqlPersonRepository(ISqlConnectionFactory sqlConnectionFactory, ISqlDynamicParametersBuilder sqlDynamicParametersBuilder)
        {
            _sqlConnectionFactory = sqlConnectionFactory ?? throw new ArgumentNullException(nameof(sqlConnectionFactory));
            _sqlDynamicParametersBuilder = sqlDynamicParametersBuilder ?? throw new ArgumentNullException(nameof(sqlDynamicParametersBuilder));
        }

        public void CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                var p = _sqlDynamicParametersBuilder.GetPersonDynamicParameters(model);
                connection.Execute("dbo.spRequestedPeople_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }


        public PersonModel? GetPersonById(int personId)
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {                
                    return connection.QueryFirstOrDefault<PersonModel>("dbo.spRequestedPeople_GetById",
                                                   new { Id = personId }, commandType: CommandType.StoredProcedure);              
            }
        }
      
        public List<PersonModel> GetRequestedPeople_All()
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                return connection.Query<PersonModel>("dbo.spRequestedPeople_GetAll").ToList();
            }
        }

        public List<PersonModel> GetCompletedPeople_All()
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                return connection.Query<PersonModel>("dbo.spCompletedPeople_GetAll").ToList();
            }
        }

        public void MoveRequestedPersonToCompleted(int personId)
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                var person = GetPersonById(personId);

                if (person != null)
                {
                    connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);

                    var parameters = _sqlDynamicParametersBuilder.GetPersonDynamicParameters(person);
                    connection.Execute("dbo.spCompletedPeople_Insert", parameters, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public void DeletePersonFromRequested(int personId)
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure); 
            }
        }

        public void DeletePersonFromCompleted(int personId)
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                connection.Execute("dbo.spCompletedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);
            }
        }

        public decimal GetTotalGrossIncomeFromCompleted()
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                return connection.Query<decimal>("dbo.spCompletedPeople_GetTotalGrossIncome").FirstOrDefault();
            }
        }

        public decimal GetTotalMetricAmountFromCompleted()
        {
            using (IDbConnection connection = _sqlConnectionFactory.CreateSqlConnection())
            {
                return connection.Query<decimal>("dbo.spCompletedPeople_GetTotalMetricAmount").FirstOrDefault();
            }
        }
    }
}
