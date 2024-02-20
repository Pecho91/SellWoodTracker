using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker;

namespace SellWoodTracker.GlobalConfig
{
    public  class GlobalConfiguration : IGlobalConfig
    {
        private readonly InitializeGlobalConnection _initializeGlobalConnection;
        
        public GlobalConfiguration()
        {
            
        }

        public string CnnString(string name)
        {
            //return ConfigurationManager.ConnectionStrings[name].ConnectionString;

            var connectionString = ConfigurationManager.ConnectionStrings[name];
            return connectionString != null ? connectionString.ConnectionString : string.Empty;
        }

        public string AppKeyLookup(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
