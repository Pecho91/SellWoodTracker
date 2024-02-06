using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker.DataAccess;

namespace SellWoodTracker
{
    public  class GlobalConfig : IGlobalConfig
    {
        private  DatabaseType _chosenDatabase = DatabaseType.Sql;
        public  DatabaseType ChosenDatabase
        {
            get { return _chosenDatabase; }
        }
       
        public  IDataConnection? Connection { get; private set; }

        public GlobalConfig()
        {
            InitializeConnections(_chosenDatabase);
        }

        public void InitializeConnections (DatabaseType databaseType)
        {
            _chosenDatabase = databaseType;

            switch (databaseType)
            {
                case DatabaseType.Sql:
                    Connection = new SqlConnector(this);
                    break;

                case DatabaseType.ExcelFile:
                    Connection = new ExcelConnector(this);
                    break;

                default:
                    throw new ArgumentException("Invalid database type provided.");
            }
        }
    

        public string CnnString(string name)
        {
            //return ConfigurationManager.ConnectionStrings[name].ConnectionString;

            var connectionString = ConfigurationManager.ConnectionStrings[name];
            return connectionString != null ? connectionString.ConnectionString : string.Empty;
        }

        public string? AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
