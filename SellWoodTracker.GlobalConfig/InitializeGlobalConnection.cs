using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.GlobalConfig
{
    public class InitializeGlobalConnection : IGlobalConnector
    {
        
        
        private DatabaseType _chosenDatabase = DatabaseType.Sql;

        ISqlPersonRepository _sqlPersonRepositoryConnection;
        public DatabaseType ChosenDatabase
        {
            get { return _chosenDatabase; }
        }
        public void InitializeConnections()
        {
            switch (_chosenDatabase)
            {
                case DatabaseType.Sql:
                    _sqlPersonRepositoryConnection = new SqlPersonService(this);
                    break;

                //case DatabaseType.ExcelFile:
                //    _sqlPersonRepositoryConnection = new ExcelPersonService(this);
                //    break;

                default:
                    throw new ArgumentException("Invalid database type provided.");
            }
        }


    }
}
//TODO move this to services? separate this to 2 classes (sql, excel).