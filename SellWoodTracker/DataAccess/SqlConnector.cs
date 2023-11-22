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
        /// <summary>
        /// Saves a new people to database
        /// </summary>
        /// <param name="model">The people information</param>
        /// <returns>The people information, including the unique identifier</returns>
        /// 
        public void CreatePerson(PersonModel model)
        {
           
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@CellphoneNumber", model.CellphoneNumber);
                p.Add("@EmailAddress", model.EmailAddress);
                
                if (DateTime.TryParseExact(model.Date, "dd.MM.yyyy.", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    p.Add("@Date", parsedDate, DbType.Date);
                }
                else
                {
                    throw new ArgumentException("Invalid date format");
                }

                p.Add("@MetricAmount", model.MetricAmount);
                p.Add("@MetricPrice", model.MetricPrice);                
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
    }  

}
