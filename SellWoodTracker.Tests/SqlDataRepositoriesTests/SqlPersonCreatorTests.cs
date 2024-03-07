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
    public class SqlPersonCreatorTests
    {
        [Fact]
        public void CreatePerson_Should_Call_Repository_Method()
        {
            // Arrange
            var model = new PersonModel
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                CellphoneNumber = "123456789",
                DateTime = DateTime.Now,
                MetricAmount = 10,
                MetricPrice = 5
            };
            var repositoryMock = new Mock<ISqlPersonCreator>();
            repositoryMock.Setup(repo => repo.CreatePerson(model)).Verifiable();

            // Act
            repositoryMock.Object.CreatePerson(model);

            // Assert
            repositoryMock.Verify(repo => repo.CreatePerson(model), Times.Once);
        }
    } 
}
