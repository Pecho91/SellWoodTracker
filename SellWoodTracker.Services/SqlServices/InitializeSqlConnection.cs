using SellWoodTracker.DataAccess.SqlDataRepository;
using SellWoodTracker.Services.SqlServices;
using SellWoodTracker.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SellWoodTracker.Services.SqlServices;

public class InitializeSqlConnection : ISqlConnector
{


    private readonly DatabaseType _chosenDatabase = DatabaseType.Sql;
    public DatabaseType ChosenDatabase
    {
        get { return _chosenDatabase; }
    }

    private readonly ISqlPersonRepository _sqlPersonRepository;
    public ISqlPersonRepository SqlPersonRepository
    {
        get { return _sqlPersonRepository; }
    }
   
  
    public InitializeSqlConnection()
    {

    }



    public void InitializeConnections()
    {
        switch (_chosenDatabase)
        {
            case DatabaseType.Sql:
                _sqlPersonRepository = new SqlPersonService((ISqlPersonRepository)this);
                break;

            //case DatabaseType.ExcelFile:
            //    _sqlPersonRepositoryConnection = new ExcelPersonService(this);
            //    break;

            default:
                throw new ArgumentException("Invalid database type provided.");
        }
    }


}
//TODO move this to services? separate this to 2 classes (sql, excel).