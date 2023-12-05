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
        private static DatabaseType _chosenDatabase = DatabaseType.ExcelFile;
        public static DatabaseType ChosenDatabase
        {
            get { return _chosenDatabase; }
        }
        
        public static IDataConnection? Connection { get; private set; }

        public static void InitializeConnections (DatabaseType db)
        {
            switch (db)
            {
                case DatabaseType.Sql:
                    Connection = new SqlConnector();
                    break;

                case DatabaseType.ExcelFile:
                    Connection = new ExcelConnector();
                    break;

                default:
                    throw new ArgumentException("Invalid database type provided.");
            }

            _chosenDatabase = db; // Update the chosen database type
        }
    

        public static string CnnString(string name)
        {
            //return ConfigurationManager.ConnectionStrings[name].ConnectionString;

            var connectionString = ConfigurationManager.ConnectionStrings[name];
            return connectionString != null ? connectionString.ConnectionString : string.Empty;
        }

        public static string AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
