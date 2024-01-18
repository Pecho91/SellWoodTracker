using Dapper;
using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker.MVVM.ViewModel;
using System.Windows.Navigation;
using System.Globalization;

namespace SellWoodTracker.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        
        private const string db = "SellWoodTracker";
     
      
        public void CreatePerson(PersonModel model)
        {
           
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@CellphoneNumber", model.CellphoneNumber);
                p.Add("@EmailAddress", model.EmailAddress);

                if (model.DateTime.HasValue)
                {
                    p.Add("@DateTime", model.DateTime.Value, DbType.DateTime);
                    
                }
                else
                {
                    p.Add("@DateTime", DBNull.Value, DbType.DateTime);
                }

                p.Add("@MetricAmount", model.MetricAmount);
                p.Add("@MetricPrice", model.MetricPrice);
                p.Add("@GrossIncome", (model.GrossIncome = model.MetricAmount * model.MetricPrice));
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spRequestedPeople_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
            }
        }
        
        public List<PersonModel> GetRequestedPeople_All()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<PersonModel>("dbo.spRequestedPeople_GetAll").ToList();
            }

            return output;
        }

        public List<PersonModel> GetCompletedPeople_All()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<PersonModel>("dbo.spCompletedPeople_GetAll").ToList();
            }

            return output;
        }

        public void MoveRequestedPersonToCompleted(int personId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var person = connection.QueryFirstOrDefault<PersonModel>("dbo.spRequestedPeople_GetById", 
                             new { Id = personId }, commandType: CommandType.StoredProcedure);

                if (person != null)
                {
                    connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);

                    var p = new DynamicParameters();
                    p.Add("@FirstName", person.FirstName);
                    p.Add("@LastName", person.LastName);
                    p.Add("@CellphoneNumber", person.CellphoneNumber);
                    p.Add("@EmailAddress", person.EmailAddress);
                    p.Add("@DateTime", person.DateTime);
                    p.Add("@MetricAmount", person.MetricAmount);
                    p.Add("@MetricPrice", person.MetricPrice);
                    p.Add("@GrossIncome", person.GrossIncome);
                    connection.Execute("dbo.spCompletedPeople_Insert", p, commandType: CommandType.StoredProcedure);
                }
            }
        }

        public void DeletePersonFromRequested(int  personId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("dbo.spRequestedPeople_DeleteById", new {id = personId}, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeletePersonFromCompleted(int personId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("dbo.spCompletedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);
            }
        }

        public decimal GetTotalGrossIncomeFromCompleted()
        {
            decimal totalGrossIncome;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                totalGrossIncome = connection.Query<decimal>("dbo.spCompletedPeople_GetTotalGrossIncome").FirstOrDefault();
            }

            return totalGrossIncome;
        }

        public decimal GetTotalMetricAmountFromCompleted() 
        {
            decimal totalMetricAmount;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                totalMetricAmount = connection.Query<decimal>("dbo.spCompletedPeople_GetTotalMetricAmount").FirstOrDefault();
            }

            return totalMetricAmount;
        }
    }  

}
