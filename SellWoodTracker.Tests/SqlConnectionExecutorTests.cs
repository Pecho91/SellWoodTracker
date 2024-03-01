using Moq;
using SellWoodTracker.DataAccess.SqlConnectionExecutor;
using SellWoodTracker.DataAccess.SqlConnectionFactory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests
{
    public class SqlConnectionExecutorTests
    {
        [Fact]
        public void Execute_Action_CallsActionWithConnection()
        {
            // Arrange
            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());

            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            connectionFactoryMock.Setup(f => f.CreateSqlConnection()).Returns(connectionMock.Object);

            ISqlConnectionExecutor executor = new SqlConnectionExecutor(connectionFactoryMock.Object);
            //var executor = new SqlConnectionExecutor(connectionFactoryMock.Object);

            bool actionCalled = false;
            Action<IDbConnection> action = conn =>
            {
                actionCalled = true;
                Assert.Equal(connectionMock.Object, conn);
            };

            // Act
            executor.Execute(action);

            // Assert
            Assert.True(actionCalled);
            connectionMock.Verify(c => c.Open(), Times.Once);
        }

        [Fact]
        public void Execute_Function_CallsFunctionWithConnection()
        {
            // Arrange
            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());

            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            connectionFactoryMock.Setup(f => f.CreateSqlConnection()).Returns(connectionMock.Object);

            ISqlConnectionExecutor executor = new SqlConnectionExecutor(connectionFactoryMock.Object);
            //var executor = new SqlConnectionExecutor(connectionFactoryMock.Object);

            const int expectedResult = 42;
            Func<IDbConnection, int> executeFunction = conn =>
            {
                Assert.Equal(connectionMock.Object, conn);
                return expectedResult;
            };

            // Act
            var result = executor.Execute(executeFunction);

            // Assert
            Assert.Equal(expectedResult, result);
            connectionMock.Verify(c => c.Open(), Times.Once);
        }
    }
}
