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
        //public const string requestedPeopleFile = "RequestedPeople.xlsx";
        //public const string connectedPeopleFile = "ConnectedPeople.xlsx";
        public const string ExcelFilePath = "C:/Users/andri/OneDrive/Documents/SellWoodTracker.xlsx";
        public static IDataConnection? Connection { get; private set; }

        public static void InitializeConnections (DatabaseType db)
        {
            if (db == DatabaseType.Sql)
            {
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }

            //if (db == DatabaseType.ExcelFile)
            //{
            //    ExcelConnector excelFile = new ExcelConnector(ExcelFilePath);
            //    Connection = excelFile;
            //}
        }

        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public static string AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
