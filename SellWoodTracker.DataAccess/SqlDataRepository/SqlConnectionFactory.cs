using SellWoodTracker.GlobalConfig;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.DataAccess.SqlDataRepository
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly IGlobalConfig _globalConfig;
        private readonly string _dataBase;

        public SqlConnectionFactory(IGlobalConfig globalConfig)
        {
            _globalConfig = globalConfig ?? throw new ArgumentNullException(nameof(globalConfig));
            _dataBase = _globalConfig.CnnString("SellWoodTracker");         
        }

        public IDbConnection CreateSqlConnection()
        {
            return new System.Data.SqlClient.SqlConnection(_dataBase);
        }
    }

    //public class MockSqlConnectionFactory : ISqlConnectionFactory
    //{
    //    public IDbConnection CreateSqlConnection()
    //    {
    //        // Return a mock IDbConnection instance
    //        return new Mock<IDbConnection>().Object;
    //    }
    //}
}
