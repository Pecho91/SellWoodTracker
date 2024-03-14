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
        public void Execute_Action_ShouldOpenConnectionAndInvokeAction()
        {            
            // Arrange
            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());

            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            connectionFactoryMock.Setup(f => f.CreateSqlConnection()).Returns(connectionMock.Object);

            var executor = new SqlConnectionExecutor(connectionFactoryMock.Object);

            bool actionInvoked = false;
            Action<IDbConnection> testAction = conn =>
            {
                actionInvoked = true;
                Assert.Equal(connectionMock.Object, conn);
            };

            // Act
            executor.Execute(testAction);

            // Assert
            Assert.True(actionInvoked);
            connectionMock.Verify(c => c.Open(), Times.Once);
        }

        [Fact]
        public void Execute_Function_ShouldOpenConnectionAndInvokeFunction()
        {
            // Arrange
            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Open());

            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            connectionFactoryMock.Setup(f => f.CreateSqlConnection()).Returns(connectionMock.Object);

            var executor = new SqlConnectionExecutor(connectionFactoryMock.Object);

           
            const int expectedResult = 42;
            Func<IDbConnection, int> testFunction = conn =>
            {
                Assert.Equal(connectionMock.Object, conn);
                return expectedResult;
            };

            // Act
            var result = executor.Execute(testFunction);

            // Assert
            Assert.Equal(expectedResult, result);
            connectionMock.Verify(c => c.Open(), Times.Once);
        }

        [Fact]
        public void Execute_ShouldHandleInvalidConnectionString()
        {
            // Arrange
            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            connectionFactoryMock.Setup(f => f.CreateSqlConnection()).Throws<ArgumentException>();

            var executor = new SqlConnectionExecutor(connectionFactoryMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => executor.Execute(conn => { /* Action not relevant */ }));
        }
    }
}
