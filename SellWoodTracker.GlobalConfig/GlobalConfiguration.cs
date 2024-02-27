using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker;

namespace SellWoodTracker.GlobalConfig
{
    public  class GlobalConfiguration : IGlobalConfiguration
    {   
        public GlobalConfiguration()
        {
            
        }

        public string CnnString(string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[name];
            return connectionString != null ? connectionString.ConnectionString : string.Empty;
        }

        public string AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
