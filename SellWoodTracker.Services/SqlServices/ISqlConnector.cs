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
    ISqlPersonService SqlPersonService { get; }
    //void InitializeConnections();
}  
