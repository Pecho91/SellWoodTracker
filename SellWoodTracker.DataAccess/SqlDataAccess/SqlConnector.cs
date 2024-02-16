using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellWoodTracker.GlobalConfig;
using SellWoodTracker.Common;

namespace SellWoodTracker.DataAccess.SqlDataAccess
{
    public class SqlConnector
    {
        private readonly IGlobalConfig _globalConfig;
        private string _dataBase;
        private readonly SqlDataOperations _sqlDataOperations;

        public SqlConnector(IGlobalConfig globalConfig, SqlDataOperations sqlDataOperations)
        {
            _globalConfig = globalConfig ?? throw new ArgumentNullException(nameof(globalConfig));
            _dataBase = _globalConfig.CnnString("SellWoodTracker");
            _sqlDataOperations = sqlDataOperations ?? throw new ArgumentNullException(nameof(sqlDataOperations));
        }

        public void CreatePerson(PersonModel model)
        {

        }
    }
}
