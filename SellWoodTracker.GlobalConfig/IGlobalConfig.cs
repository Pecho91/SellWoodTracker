using SellWoodTracker.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.GlobalConfig
{
    public interface IGlobalConfig
    {
        DatabaseType ChosenDatabase { get; }
       
        string CnnString(string name);
        string? AppKeyLookup(string key);
    }
}
