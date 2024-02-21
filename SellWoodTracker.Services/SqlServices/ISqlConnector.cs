using SellWoodTracker.Common.Enums;
using SellWoodTracker.DataAccess.SqlDataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Services.SqlServices;

public interface ISqlConnector

{
    DatabaseType ChosenDatabase { get; }
    ISqlPersonRepository SqlPersonRepository { get; }
    void InitializeConnections();
}  
// TODO move this to services?