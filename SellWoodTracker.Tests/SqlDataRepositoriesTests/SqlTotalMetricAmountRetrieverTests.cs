using Moq;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlDataRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests.SqlDataRepositoriesTests
{
    public class SqlTotalMetricAmountRetrieverTests
    {
        [Fact]
        public void GetTotalMetricAmountFromCompleted_Returns_CorrectValue()
        {
            // Arrange
            decimal expectedMetricAmount = 123.45m; 

            var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            sqlConnectionExecutorMock.Setup(mock =>
                mock.Execute(It.IsAny<Func<IDbConnection, decimal>>()))
                .Returns(expectedMetricAmount);

            var sqlTotalMetricAmountRetriever = new SqlTotalMetricAmountRetriever(sqlConnectionExecutorMock.Object);

            // Act
            decimal actualMetricAmount = sqlTotalMetricAmountRetriever.GetTotalMetricAmountFromCompleted();

            // Assert          
            Assert.InRange(actualMetricAmount, expectedMetricAmount - 0.01m, expectedMetricAmount + 0.01m);
        }
    }
}
