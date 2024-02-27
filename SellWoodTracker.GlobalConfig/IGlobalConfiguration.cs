
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.GlobalConfig
{
    public interface IGlobalConfiguration
    {      
        string CnnString(string name);
        string AppKeyLookup(string key);
    }
}
