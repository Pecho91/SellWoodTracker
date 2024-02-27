using Moq;
using SellWoodTracker.DataAccess.SqlDataRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests.Mocks
{
    public class MockSqlConnectionFactory : ISqlConnectionFactory
    {
        public IDbConnection CreateSqlConnection()
        {
            throw (Exception) new Mock<IDbConnection>().Object;
        }
    }
}
