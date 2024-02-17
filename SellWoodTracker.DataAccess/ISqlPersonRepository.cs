using Dapper;
using SellWoodTracker.Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess
{
    public interface ISqlPersonRepository
    {
        void CreatePerson(PersonModel model);
        public PersonModel GetPersonById(IDbConnection connection, int personId);
        List<PersonModel> GetRequestedPeople_All();
        List<PersonModel> GetCompletedPeople_All();
        void MoveRequestedPersonToCompleted(int personId);
        void DeletePersonFromRequested(int personId);
        void DeletePersonFromCompleted(int personId);
        decimal GetTotalGrossIncomeFromCompleted();
        decimal GetTotalMetricAmountFromCompleted();

    
    }
}
//TODO

Sure, here's how you can refactor the provided code using the repository pattern:

First, define a repository interface for managing PersonModel entities:

csharp
Copy code
using SellWoodTracker.Common.Model;
using System.Collections.Generic;

namespace SellWoodTracker.DataAccess.Repositories
{
    public interface IPersonRepository
    {
        void Create(PersonModel model);
        List<PersonModel> GetAllRequestedPeople();
        List<PersonModel> GetAllCompletedPeople();
        void MoveToCompleted(int personId);
        void DeleteFromRequested(int personId);
        void DeleteFromCompleted(int personId);
        decimal GetTotalGrossIncomeFromCompleted();
        decimal GetTotalMetricAmountFromCompleted();
    }
}
Then, implement this interface in a concrete repository class:

csharp
Copy code
using Dapper;
using SellWoodTracker.Common.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SellWoodTracker.DataAccess.Repositories
{
    public class SqlPersonRepository : IPersonRepository
    {
        private readonly IDbConnection _connection;

        public SqlPersonRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Create(PersonModel model)
        {
            var parameters = GetDynamicParameters(model);
            _connection.Execute("dbo.spRequestedPeople_Insert", parameters, commandType: CommandType.StoredProcedure);
            model.Id = parameters.Get<int>("@id");
        }

        public List<PersonModel> GetAllRequestedPeople()
        {
            return _connection.Query<PersonModel>("dbo.spRequestedPeople_GetAll").ToList();
        }

        public List<PersonModel> GetAllCompletedPeople()
        {
            return _connection.Query<PersonModel>("dbo.spCompletedPeople_GetAll").ToList();
        }

        public void MoveToCompleted(int personId)
        {
            var person = _connection.QueryFirstOrDefault<PersonModel>("dbo.spRequestedPeople_GetById",
                             new { Id = personId }, commandType: CommandType.StoredProcedure);

            if (person != null)
            {
                _connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);

                var parameters = GetDynamicParameters(person);
                _connection.Execute("dbo.spCompletedPeople_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
        }
        
        // TODO
        public void DeleteFromRequested(int personId)
        {
            _connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);
        }

        public void DeleteFromCompleted(int personId)
        {
            _connection.Execute("dbo.spCompletedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);
        }

        public decimal GetTotalGrossIncomeFromCompleted()
        {
            return _connection.Query<decimal>("dbo.spCompletedPeople_GetTotalGrossIncome").FirstOrDefault();
        }

        public decimal GetTotalMetricAmountFromCompleted()
        {
            return _connection.Query<decimal>("dbo.spCompletedPeople_GetTotalMetricAmount").FirstOrDefault();
        }

        private DynamicParameters GetDynamicParameters(PersonModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", model.FirstName);
            parameters.Add("@LastName", model.LastName);
            parameters.Add("@CellphoneNumber", model.CellphoneNumber);
            parameters.Add("@EmailAddress", model.EmailAddress);
            parameters.Add("@DateTime", model.DateTime ?? DBNull.Value, DbType.DateTime);
            parameters.Add("@MetricAmount", model.MetricAmount);
            parameters.Add("@MetricPrice", model.MetricPrice);
            parameters.Add("@GrossIncome", model.GrossIncome = model.MetricAmount * model.MetricPrice);
            parameters.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            return parameters;
        }
    }
}