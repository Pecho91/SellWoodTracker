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
    public class SqlConnector
    {
        private readonly IGlobalConfig _globalConfig;
        private readonly string _dataBase;
        private readonly SqlDataOperations _sqlDataOperations;

        public SqlConnector(IGlobalConfig globalConfig, SqlDataOperations sqlDataOperations)
        {
            
            _globalConfig = globalConfig ?? throw new ArgumentNullException(nameof(globalConfig));
            _dataBase = _globalConfig.CnnString("SellWoodTracker");
            _sqlDataOperations = sqlDataOperations ?? throw new ArgumentNullException(nameof(sqlDataOperations));
        }

        public void CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_dataBase)) 
            {
                var p = _sqlDataOperations.CreatePerson(model);
                connection.Execute("dbo.spRequestedPeople_Insert", p, commandType: CommandType.StoredProcedure);
                model.Id = p.Get<int>("@id");
            }
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
    }
}
