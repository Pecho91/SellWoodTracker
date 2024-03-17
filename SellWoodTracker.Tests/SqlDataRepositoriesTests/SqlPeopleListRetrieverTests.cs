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
    public class SqlPeopleListRetrieverTests
    {

        
        [Fact]
        public void GetRequestedPeople_All_ShouldRetrieveRequestedPeople()
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

            var sqlPeopleRetriever = new SqlPeopleListRetriever(sqlConnectionExecutorMock.Object);

            // Act
            var result = sqlPeopleRetriever.GetRequestedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, result);
        }
       
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

            var executorMock = new Mock<ISqlConnectionExecutor>();
            executorMock.Setup(e => e.Execute(It.IsAny<Func<IDbConnection, List<PersonModel>>>()))
                        .Returns(expectedPeople);

            var retriever = new SqlPeopleListRetriever(executorMock.Object);

            // Act
            var result = retriever.GetCompletedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, result);
        }
    }
}
