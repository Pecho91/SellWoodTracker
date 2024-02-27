

using Dapper;
using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlDataAccess;
using SellWoodTracker.DataAccess.SqlDataRepository;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using SellWoodTracker.GlobalConfig;
using SellWoodTracker.Tests.Mocks;
using System.Data;


namespace SellWoodTracker.Tests
{
    public class SqlPersonRepositoryTests
    {
        [Fact]
        public void CreatePerson_WhenCalled_ShouldExecuteCorrectly()
        {
            //var sqlConnectionFactoryMock = new Mock<ISqlConnectionFactory>().Object;
            var sqlDynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();

            var repository = new SqlPersonRepository(sqlDynamicParametersBuilderMock.Object);

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

           // repository.CreatePerson(personModel);

            //sqlConnectionFactoryMock.Verify(mock => mock.CreateSqlConnection(), Times.Once());
            sqlDynamicParametersBuilderMock.Verify(mock => mock.GetPersonDynamicParameters(personModel), Times.Once());
        }
    }
}