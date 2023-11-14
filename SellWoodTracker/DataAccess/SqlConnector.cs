﻿using Dapper;
using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker.MVVM.ViewModel;
using System.Data.SqlClient;
using System.Windows.Navigation;

namespace SellWoodTracker.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "SellWoodDatabase";
        /// <summary>
        /// Saves a new people to database
        /// </summary>
        /// <param name="model">The people information</param>
        /// <returns>The people information, including the unique identifier</returns>
        /// 
        public void CreatePerson(PersonModel model)
        {
            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@CellphoneNumber", model.CellphoneNumber);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@Date", model.Date);
                p.Add("@MetricAmount", model.MetricAmount);
                p.Add("@MetricPrice", model.MetricPrice);                
                p.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPerson_Insert", p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
            }
        }

        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;

            using (IDbConnection connection = new SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }

            return output;
        }

        
    }  

}