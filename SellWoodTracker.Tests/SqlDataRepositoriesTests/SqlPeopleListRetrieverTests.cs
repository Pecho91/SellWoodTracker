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
        public void GetRequestedPeople_All_Should_Return_List_Of_People()
        {
            // Arrange
            var expectedPeople = new List<PersonModel>
            {
                new PersonModel
                {
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "john.doe@example.com",
                    CellphoneNumber = "123456789",
                    DateTime = DateTime.Now,
                    MetricAmount = 10,
                    MetricPrice = 5
                },

                new PersonModel
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    EmailAddress = "jane.doe@example.com",
                    CellphoneNumber = "987654321",
                    DateTime = DateTime.Now,
                    MetricAmount = 20,
                    MetricPrice = 8
                },
             };

            var repositoryMock = new Mock<ISqlPeopleListRetriever>();
            repositoryMock.Setup(repo => repo.GetRequestedPeople_All()).Returns(expectedPeople);

            // Act
            var actualPeople = repositoryMock.Object.GetRequestedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, actualPeople);
        }

        [Fact]
        public void GetRequestedPeople_All_ShouldRetrieveRequestedPeople()
        {
            // TODO
            // Arrange
            var expectedPeople = new List<PersonModel>
            {
                new PersonModel { Id = 1, FirstName = "Alice" },
                new PersonModel { Id = 2, FirstName = "Bob" }
                
                // Add more expected people as needed
            };

            var connectionMock = new Mock<IDbConnection>();
            connectionMock.Setup(c => c.Query<PersonModel>("dbo.spRequestedPeople_GetAll", null, null, true, null, null))
                          .Returns(expectedPeople);

            var executorMock = new Mock<ISqlConnectionExecutor>();
            executorMock.Setup(e => e.Execute(It.IsAny<Func<IDbConnection, List<PersonModel>>>()))
                        .Returns(expectedPeople);

            var retriever = new SqlPeopleListRetriever(executorMock.Object);

            // Act
            var result = retriever.GetRequestedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, result);
        }

        [Fact]
        public void GetRequestedPeople_All_ShouldRetrieveRequestedPeople1()
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
            var result = retriever.GetRequestedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, result);
        }
    }
}
