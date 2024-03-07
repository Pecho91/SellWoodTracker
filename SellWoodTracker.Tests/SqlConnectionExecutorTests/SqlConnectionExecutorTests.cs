using Moq;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlConnectionFactorys;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests.SqlConnectionExecutorTests
{
    public class SqlConnectionExecutorTests
    {
        [Fact]
        public void Execute_Action_CallsActionWithConnection()
        {
            // TODO -mocking or not???
            // Arrange
            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());

            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            connectionFactoryMock.Setup(f => f.CreateSqlConnection()).Returns(connectionMock.Object);

            ISqlConnectionExecutor executor = new SqlConnectionExecutor(connectionFactoryMock.Object);

            // Use reflection to set the private field _sqlConnectionFactory to the mocked instance
            var field = typeof(SqlConnectionExecutor).GetField("_sqlConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(executor, connectionFactoryMock.Object);

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

            var field = typeof(SqlConnectionExecutor).GetField("_sqlConnectionFactory", BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(executor, connectionFactoryMock.Object);

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
