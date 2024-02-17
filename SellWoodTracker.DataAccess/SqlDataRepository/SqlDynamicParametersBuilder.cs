using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker.MVVM.ViewModel;
using System.Windows.Navigation;
using System.Globalization;
using SellWoodTracker.GlobalConfig;
using SellWoodTracker.Common.Model;

namespace SellWoodTracker.DataAccess.SqlDataAccess
{
    public class SqlDynamicParametersBuilder : ISqlPersonRepository
    {

        private readonly IGlobalConfig _globalConfig;
        private readonly string _dataBase;

        public SqlDynamicParametersBuilder()
        {
            
            _dataBase = _globalConfig.CnnString("SellWoodTracker");
        }

        
        public DynamicParameters BuildParametersForPerson(PersonModel model)
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
                    p.Add("@GrossIncome", model.GrossIncome = model.MetricAmount * model.MetricPrice);
                    p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                    return p;
                }


      
        //TODO


        public void MoveRequestedPersonToCompleted(IDbConnection connection, PersonModel person)
        {
            var p = new DynamicParameters();
            p.Add("@FirstName", person.FirstName);
            p.Add("@LastName", person.LastName);
            p.Add("@CellphoneNumber", person.CellphoneNumber);
            p.Add("@EmailAddress", person.EmailAddress);
            p.Add("@DateTime", person.DateTime);
            p.Add("@MetricAmount", person.MetricAmount);
            p.Add("@MetricPrice", person.MetricPrice);
            p.Add("@GrossIncome", person.GrossIncome);
            connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = person.Id }, commandType: CommandType.StoredProcedure);
            connection.Execute("dbo.spCompletedPeople_Insert", p, commandType: CommandType.StoredProcedure);
        }

        public void DeletePersonFromRequested(int personId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_dataBase))
            {
                connection.Execute("dbo.spRequestedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeletePersonFromCompleted(int personId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_dataBase))
            {
                connection.Execute("dbo.spCompletedPeople_DeleteById", new { id = personId }, commandType: CommandType.StoredProcedure);
            }
        }

        public decimal GetTotalGrossIncomeFromCompleted()
        {
            decimal totalGrossIncome;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_dataBase))
            {
                totalGrossIncome = connection.Query<decimal>("dbo.spCompletedPeople_GetTotalGrossIncome").FirstOrDefault();
            }

            return totalGrossIncome;
        }

        public decimal GetTotalMetricAmountFromCompleted()
        {
            decimal totalMetricAmount;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_dataBase))
            {
                totalMetricAmount = connection.Query<decimal>("dbo.spCompletedPeople_GetTotalMetricAmount").FirstOrDefault();
            }

            return totalMetricAmount;
        }
    }

}
