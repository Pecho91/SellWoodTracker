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
    public class SqlPersonRetrieverTests
    {
        [Fact]
        public void GetPersonById_ReturnsPersonModel()
        {
            var personRepositoryMock = new Mock<ISqlPersonRetriever>();

            int personId = 72;
            string firstName = "sada";

            var expectedPerson = new PersonModel { Id = personId, FirstName = firstName };

            personRepositoryMock.Setup(mock => mock.GetPersonById(personId)).Returns(expectedPerson);

            var result = personRepositoryMock.Object.GetPersonById(personId);

            Assert.NotNull(result);
            Assert.IsType<PersonModel>(result);
            Assert.Equal(expectedPerson.Id, result.Id);
            Assert.Equal(expectedPerson.FirstName, result.FirstName);
        }
        [Fact]
        public void GetPersonById_Returns_PersonModel()
        {
            // Arrange
            int personId = 1;
            var expectedPerson = new PersonModel { Id = 1, FirstName = "John" };

            var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            sqlConnectionExecutorMock.Setup(mock => mock.Execute(It.IsAny<Func<IDbConnection, PersonModel>>()))
                                  .Returns(expectedPerson);

            var sqlPersonRetriever = new SqlPersonRetriever(sqlConnectionExecutorMock.Object);

            // Act
            var result = sqlPersonRetriever.GetPersonById(personId);

            // Assert   
            Assert.Equal(expectedPerson, result);
        }

        [Fact]
        public void GetPersonById_Returns_Null_When_Person_Not_Found()
        {
            // Arrange
            int personId = 1;

            var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            sqlConnectionExecutorMock.Setup(mock => mock.Execute(It.IsAny<Func<IDbConnection, PersonModel>>()))
                                  .Returns<PersonModel>(null);

            var sqlPersonRetriever = new SqlPersonRetriever(sqlConnectionExecutorMock.Object);

            // Act
            var result = sqlPersonRetriever.GetPersonById(personId);

            // Assert
            Assert.Null(result);
        }
    }
}
