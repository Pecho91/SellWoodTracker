using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker.DataAccess;

namespace SellWoodTracker
{
    public static class GlobalConfig
    {
        public static IDataConnection? Connection { get; set; }
    }
}
