

using Dapper;
using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlDataAccess;
using SellWoodTracker.DataAccess.SqlDataRepository;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using SellWoodTracker.GlobalConfig;

using System.Data;


namespace SellWoodTracker.Tests
{
    public class SqlPersonRepositoryTests
    {
        //[Fact]
        //public void Constructor_WithConnectionFactory_ShouldExecuteCorrectly()
        //{
        //    // Arrange
        //    var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
        //    var repository = new SqlPersonRepository(connectionFactoryMock.Object);

        //    // Act & Assert
        //    Assert.NotNull(repository);
        //}

        //[Fact]
        //public void Constructor_WithDynamicParametersBuilder_ShouldExecuteCorrectly()
        //{
        //    // Arrange
        //    var dynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();
        //    var repository = new SqlPersonRepository(dynamicParametersBuilderMock.Object);

        //    // Act & Assert
        //    Assert.NotNull(repository);
        //}

        [Fact]
        public void Constructor_WithBothConnectionFactoryAndDynamicParametersBuilder_ShouldExecuteCorrectly()
        {
            // Arrange
            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            var dynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();
            var repository = new SqlPersonRepository(connectionFactoryMock.Object, dynamicParametersBuilderMock.Object);

            // Act & Assert
            Assert.NotNull(repository);
            Assert.Same(connectionFactoryMock.Object, repository.SqlConnectionFactory);
            Assert.Same(dynamicParametersBuilderMock.Object, repository.SqlDynamicParametersBuilder);
        }
        [Fact]
        public void CreatePerson_WhenCalled_ShouldExecuteCorrectly()
        {
            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            var dynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();       

            var repository = new SqlPersonRepository(connectionFactoryMock.Object, dynamicParametersBuilderMock.Object);

            var personModel = new PersonModel
            {
                FirstName = "John",
                LastName = "Doe",
                CellphoneNumber = "123456789",
                EmailAddress = "john.doe@example.com",
                DateTime = DateTime.Now, // Assuming DateTime property is required
                MetricAmount = 10,
                MetricPrice = 5
            };

            repository.CreatePerson(personModel);

           // connectionFactoryMock.Verify(mock => mock.CreateSqlConnection(), Times.Once());
            dynamicParametersBuilderMock.Verify(mock => mock.GetPersonDynamicParameters(personModel), Times.Once());
        }
    }
}