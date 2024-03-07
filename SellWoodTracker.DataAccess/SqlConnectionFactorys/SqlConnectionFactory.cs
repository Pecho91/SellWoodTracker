using SellWoodTracker.GlobalConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlConnectionFactorys
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
           
        private readonly IGlobalConfiguration _globalConfig;
        private readonly string _dataBase;

        public SqlConnectionFactory(IGlobalConfiguration globalConfiguration)
        {
            //_globalConfig = new GlobalConfiguration();
            _globalConfig = globalConfiguration ?? throw new ArgumentNullException(nameof(globalConfiguration));
            _dataBase = _globalConfig.CnnString("SellWoodTracker");
        }

        public IDbConnection CreateSqlConnection()
        {
            return new System.Data.SqlClient.SqlConnection(_dataBase);
        }
    }
}
