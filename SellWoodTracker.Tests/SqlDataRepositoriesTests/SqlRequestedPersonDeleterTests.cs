using Dapper;
using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using SellWoodTracker.DataAccess.SqlDataRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests.SqlDataRepositoriesTests
{
    public class SqlRequestedPersonDeleterTests
    {
        [Fact]
        public void DeletePersonFromRequested_Deletes_Person()
        {
            // Arrange
            int personId = 42;
            var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            var sqlPersonDeleter = new SqlRequestedPersonDeleter(sqlConnectionExecutorMock.Object);

            // Act
            sqlPersonDeleter.DeletePersonFromRequested(personId);

            // Assert
            sqlConnectionExecutorMock.Verify(executor => executor.Execute(It.IsAny<Action<IDbConnection>>()), Times.Once);

            
        }       
    }
}