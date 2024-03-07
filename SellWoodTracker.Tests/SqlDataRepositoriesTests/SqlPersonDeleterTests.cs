using Dapper;
using Moq;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using SellWoodTracker.DataAccess.SqlDataRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests.SqlDataRepositoriesTests
{
    public class SqlPersonDeleterTests
    {
        [Fact]
        public void DeletePersonFromRequested_DeletesPersonFromRequestedTable()
        {
            // Arrange
           // var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            var personDeleterMock = new Mock<ISqlPersonDeleter>();
            int personId = 72;

            // Setting up the mock
            personDeleterMock.Setup(deleter => deleter.DeletePersonFromRequested(personId));

            // Act
            personDeleterMock.Object.DeletePersonFromRequested(personId);

            // Assert
            // Verify that the DeletePersonFromRequested method was called exactly once with the provided personId
            personDeleterMock.Verify(deleter => deleter.DeletePersonFromRequested(personId), Times.Once);
        }

        [Fact]
        public void DeletePersonFromCompleted_DeletesPersonFromRequestedTable()
        {
            // Arrange
            //var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            var personDeleterMock = new Mock<ISqlPersonDeleter>();
            int personId = 72;

            // Setting up the mock
            personDeleterMock.Setup(deleter => deleter.DeletePersonFromCompleted(personId));

            // Act
            personDeleterMock.Object.DeletePersonFromCompleted(personId);

            // Assert
            // Verify that the DeletePersonFromRequested method was called exactly once with the provided personId
            personDeleterMock.Verify(deleter => deleter.DeletePersonFromCompleted(personId), Times.Once);
        }
    }
}