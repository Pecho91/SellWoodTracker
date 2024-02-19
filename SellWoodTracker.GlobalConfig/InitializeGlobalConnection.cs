using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.GlobalConfig
{
    public class InitializeGlobalConnection : IGlobalConnections
    {
        private readonly SqlConnectionFactory sqlConnectionFactory;
        private DatabaseType _chosenDatabase = DatabaseType.ExcelFile;
        public DatabaseType ChosenDatabase
        {
            get { return _chosenDatabase; }
        }
        public void InitializeConnections()
        {
            switch (_chosenDatabase)
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
    }
}
//TODO