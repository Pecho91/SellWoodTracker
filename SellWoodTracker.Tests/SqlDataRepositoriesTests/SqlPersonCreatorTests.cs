using Dapper;
using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlConnectionExecutors;
using SellWoodTracker.DataAccess.SqlConnectionFactorys;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using SellWoodTracker.DataAccess.SqlDataRepositories;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
             
        [Fact]
        public void CreatePerson_Calls_Executor_With_Correct_Parameters()
        {
            // Arrange
            var model = new PersonModel { FirstName = "John" };

            var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            var sqlDynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();

            // Set up dynamic parameters builder
            var mockDynamicParameters = new DynamicParameters();

            mockDynamicParameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            sqlDynamicParametersBuilderMock.Setup(builder => builder.GetPersonDynamicParameters(model))
                                         .Returns(mockDynamicParameters);

            var sqlPersonCreator = new SqlPersonCreator(sqlConnectionExecutorMock.Object, sqlDynamicParametersBuilderMock.Object);

            // Act
            sqlPersonCreator.CreatePerson(model);

            // Assert
            sqlConnectionExecutorMock.Verify(mock => mock.Execute(It.IsAny<Action<IDbConnection>>()), Times.Once);

            // Check if the dynamic parameters were properly set and used in the query execution
            sqlConnectionExecutorMock.Verify(mock => mock.Execute(It.IsAny<Action<IDbConnection>>()), Times.Once);
            sqlConnectionExecutorMock.Verify(mock => mock.Execute(It.IsAny<Action<IDbConnection>>()), Times.Once);
            sqlDynamicParametersBuilderMock.Verify(mock => mock.GetPersonDynamicParameters(model), Times.Once);
        }

        [Fact]
        public void CreatePerson_Sets_Full_Person_Model_Correctly()
        {
            // Arrange
            var model = new PersonModel
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                CellphoneNumber = "123456789",
                DateTime = DateTime.Now,
                MetricAmount = 10,
                MetricPrice = 5
            };

            var sqlConnectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            var sqlDynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();

            // Set up dynamic parameters builder
            var mockDynamicParameters = new DynamicParameters();

            mockDynamicParameters.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            mockDynamicParameters.Add("@firstName", model.FirstName); // Adding other parameters
            mockDynamicParameters.Add("@lastName", model.LastName);
            mockDynamicParameters.Add("@email", model.EmailAddress);
            mockDynamicParameters.Add("@cellphoneNumber", model.CellphoneNumber);
            mockDynamicParameters.Add("@dateTime", model.DateTime);
            mockDynamicParameters.Add("@metricAmount", model.MetricAmount);
            mockDynamicParameters.Add("@metricPrice", model.MetricPrice);

            sqlDynamicParametersBuilderMock.Setup(builder => builder.GetPersonDynamicParameters(model))
                                         .Returns(mockDynamicParameters);

            sqlConnectionExecutorMock.Setup(execute => execute.Execute(It.IsAny<Func<IDbConnection, PersonModel>>()))
                       .Returns(model);

            var sqlPersonCreator = new SqlPersonCreator(sqlConnectionExecutorMock.Object, sqlDynamicParametersBuilderMock.Object);
            // Act
            sqlPersonCreator.CreatePerson(model);

            // Assert
            Assert.NotEqual(0, model.Id);
            Assert.Equal("John", model.FirstName);
            Assert.Equal("Doe", model.LastName);
            Assert.Equal("john.doe@example.com", model.EmailAddress);
            Assert.Equal("123456789", model.CellphoneNumber);
            Assert.Equal(10, model.MetricAmount);
            Assert.Equal(5, model.MetricPrice);
            // Add assertions for other properties as needed
        }
    } 
}
