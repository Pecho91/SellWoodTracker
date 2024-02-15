using SellWoodTracker.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker
{
    public interface IGlobalConfig
    {
        DatabaseType ChosenDatabase { get; }
        IDataConnection? Connection { get; }
        void InitializeConnections();
        string CnnString(string name);
        string? AppKeyLookup(string key);
    }
}
