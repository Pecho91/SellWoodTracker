using Moq;
using SellWoodTracker.Common.Model;
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
    public class SqlTotalGrossIncomeRetrieverTests
    {
        [Fact]
        public void GetTotalGrossIncomeFromCompleted_Returns_CorrectValue()
        {
            // Arrange
            decimal expectedGrossIncome = 12345.67m; // Replace with the expected gross income value

            var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            sqlConnectionExecutorMock.Setup(mock =>
                mock.Execute(It.IsAny<Func<IDbConnection, decimal>>()))
                .Returns(expectedGrossIncome);

            var sqlTotalGrossIncomeRetriever = new SqlTotalGrossIncomeRetriever(sqlConnectionExecutorMock.Object);

            // Act
            decimal actualGrossIncome = sqlTotalGrossIncomeRetriever.GetTotalGrossIncomeFromCompleted();

            // Assert
            Assert.InRange(actualGrossIncome, expectedGrossIncome - 0.01m, expectedGrossIncome + 0.01m);
        }
    }
}
