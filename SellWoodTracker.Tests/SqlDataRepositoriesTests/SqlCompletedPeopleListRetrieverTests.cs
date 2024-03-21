using Dapper;
using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using SellWoodTracker.DataAccess.SqlDataRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests.SqlDataRepositoriesTests
{
    public class SqlCompletedPeopleListRetrieverTests
    {     
        [Fact]
        public void GetCompletedPeople_All_ShouldRetrieveCompletedPeople()
        {
            // Arrange
            var expectedPeople = new List<PersonModel>
            {
                new PersonModel { Id = 1, FirstName = "Alice" },
                new PersonModel { Id = 2, FirstName = "Bob" }
                // Add more expected people as needed
            };

            var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            sqlConnectionExecutorMock.Setup(e => e.Execute(It.IsAny<Func<IDbConnection, List<PersonModel>>>()))
                        .Returns(expectedPeople);

            var sqlCompletedPeopleListRetriever = new SqlCompletedPeopleListRetriever(sqlConnectionExecutorMock.Object);

            // Act
            var actualPeople = sqlCompletedPeopleListRetriever.GetCompletedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, actualPeople);
        }
    }
}
