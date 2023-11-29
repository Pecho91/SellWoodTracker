using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker.DataAccess;

namespace SellWoodTracker
{
    public static class GlobalConfig
    {
        
        //public const string ExcelFilePath = "C:/Users/andri/OneDrive/Documents/SellWoodTracker.xlsx";
        public static IDataConnection? Connection { get; private set; }

        public static void InitializeConnections (DatabaseType db)
        {
            //if (db == DatabaseType.Sql)
            //{
            //    SqlConnector sql = new SqlConnector();
            //    Connection = sql;
            //}

            // TODO sql/excel 
            if (db == DatabaseType.ExcelFile)
            {
                ExcelConnector excelFile = new ExcelConnector();
                Connection = excelFile;
            }
        }

        public static string CnnString(string name)
        {
            // return ConfigurationManager.ConnectionStrings[name].ConnectionString;

            var connectionString = ConfigurationManager.ConnectionStrings[name];
            return connectionString != null ? connectionString.ConnectionString : string.Empty;
        }

        public static string AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
