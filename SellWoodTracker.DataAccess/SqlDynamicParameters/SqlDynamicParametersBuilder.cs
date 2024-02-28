using Dapper;
using System.Data;
using SellWoodTracker.GlobalConfig;
using SellWoodTracker.Common.Model;

namespace SellWoodTracker.DataAccess.SqlDynamicParameters
{
    public class SqlDynamicParametersBuilder : ISqlDynamicParametersBuilder
    {
        public SqlDynamicParametersBuilder()
        {

        }

        public DynamicParameters GetPersonDynamicParameters(PersonModel model)
        {
            var p = new DynamicParameters();
            p.Add("@FirstName", model.FirstName);
            p.Add("@LastName", model.LastName);
            p.Add("@EmailAddress", model.EmailAddress);
            p.Add("@CellphoneNumber", model.CellphoneNumber);
            
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
       
    }

}
