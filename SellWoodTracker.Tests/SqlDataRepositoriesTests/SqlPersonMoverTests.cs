using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using SellWoodTracker.DataAccess.SqlDataRepositories;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests.SqlDataRepositoriesTests
{
    public class SqlPersonMoverTests
    {
        [Fact]
        public void MoveRequestedPersonToCompleted_Deletes_Person_If_Exists()
        {
            // Arrange
            int personId = 42; // Replace with an actual person ID
            var person = new PersonModel { Id = personId, FirstName = "John" }; // Replace with your PersonModel

            var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            var sqlDynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();
            var sqlPersonRetrieverMock = new Mock<ISqlPersonRetriever>();

            sqlPersonRetrieverMock.Setup(mock => mock.GetPersonById(personId)).Returns(person);

            var sqlPersonMover = new SqlPersonMover(
                sqlConnectionExecutorMock.Object,
                sqlDynamicParametersBuilderMock.Object,
                sqlPersonRetrieverMock.Object);

            // Act
            sqlPersonMover.MoveRequestedPersonToCompleted(personId);

            // Assert
            sqlConnectionExecutorMock.Verify(mock =>
                mock.Execute(It.IsAny<Action<IDbConnection>>()), Times.Once);
         
        }
    }
}