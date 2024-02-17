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

namespace SellWoodTracker.DataAccess.SqlDataAccess
{
    public class SqlPersonRepository : ISqlPersonRepository
    {
        private readonly IGlobalConfig _globalConfig;
        private readonly string _dataBase;
        private readonly SqlDynamicParametersBuilder _sqlDynamicParametersBuilder;

        public SqlPersonRepository(IGlobalConfig globalConfig, SqlDynamicParametersBuilder sqlDynamicParametersBuilder)
        {
            
            _globalConfig = globalConfig ?? throw new ArgumentNullException(nameof(globalConfig));
            _dataBase = _globalConfig.CnnString("SellWoodTracker");
            _sqlDynamicParametersBuilder = sqlDynamicParametersBuilder ?? throw new ArgumentNullException(nameof(sqlDynamicParametersBuilder);
        }

        public void CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_dataBase)) 
            {
                var p = _sqlDynamicParametersBuilder.BuildParametersForPerson(model);
                connection.Execute("dbo.spRequestedPeople_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
        }


        public PersonModel GetPersonById(IDbConnection connection, int personId)
        {
            return connection.QueryFirstOrDefault<PersonModel>("dbo.spRequestedPeople_GetById",
                    new { Id = personId }, commandType: CommandType.StoredProcedure);
        }

        public List<PersonModel> GetRequestedPeople_All()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_dataBase))
            {
                return connection.Query<PersonModel>("dbo.spRequestedPeople_GetAll").ToList();
            }
        }

        public List<PersonModel> GetCompletedPeople_All()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_dataBase))
            {
                return connection.Query<PersonModel>("dbo.spCompletedPeople_GetAll").ToList();
            }
        }

        public void MoveRequestedPersonToCompleted(int personId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_dataBase))
            {
                var person = _sqlDataOperations.GetPersonById(connection, personId);

                if (person != null)
                {
                    _sqlDataOperations.MoveRequestedPersonToCompleted(connection, person);
                }
            }
        }

        public void DeletePersonFromRequested(int personId)
        {
            throw new NotImplementedException();
        }

        public void DeletePersonFromCompleted(int personId)
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalGrossIncomeFromCompleted()
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalMetricAmountFromCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
