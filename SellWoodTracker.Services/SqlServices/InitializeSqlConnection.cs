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
    public DatabaseType ChosenDatabase => _chosenDatabase;

    private readonly SqlPersonService _sqlPersonService;
    public ISqlPersonService SqlPersonService => _sqlPersonService;


    public InitializeSqlConnection(ISqlPersonRepository repository)
    {
        if (_chosenDatabase == DatabaseType.Sql)
        {
            _sqlPersonService = new SqlPersonService(repository);
        }
        else
        {
            throw new ArgumentException("Invalid database type provided.");
        }
    }

    
}
//TODO ?????? a lot dependencies????