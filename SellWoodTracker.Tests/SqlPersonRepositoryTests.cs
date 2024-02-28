

using Dapper;
using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlDataAccess;
using SellWoodTracker.DataAccess.SqlDataRepository;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using SellWoodTracker.GlobalConfig;
using SellWoodTracker.Services.SqlServices;
using System.Data;


namespace SellWoodTracker.Tests
{
    public class SqlPersonRepositoryTests
    {
        
        public SqlPersonRepositoryTests()
        {
            
        }

        [Fact]
        public void Constructor_WithBothConnectionFactoryAndDynamicParametersBuilder_ShouldExecuteCorrectly()
        {           
            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            var dynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();
            var repository = new SqlPersonRepository(connectionFactoryMock.Object, dynamicParametersBuilderMock.Object);

            Assert.NotNull(repository);
            Assert.Same(connectionFactoryMock.Object, repository.SqlConnectionFactory);
            Assert.Same(dynamicParametersBuilderMock.Object, repository.SqlDynamicParametersBuilder);
        }
        [Fact]
        public void CreatePerson_WhenCalled_ShouldExecuteCorrectly()
        {
            //TODO (ISqlPersonRepository) ??
            var connectionFactoryMock = new Mock<ISqlConnectionFactory>();
            var dynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();       

            var repository = new SqlPersonRepository(connectionFactoryMock.Object, dynamicParametersBuilderMock.Object);

            var personModel = new PersonModel
            {
                FirstName = "John",
                LastName = "Doe",
                CellphoneNumber = "123456789",
                EmailAddress = "john.doe@example.com",
               // DateTime = DateTime.Now, // Assuming DateTime property is required
                MetricAmount = 10,
                MetricPrice = 5
            };

            repository.CreatePerson(personModel);

           // connectionFactoryMock.Verify(mock => mock.CreateSqlConnection(), Times.Once());
            dynamicParametersBuilderMock.Verify(mock => mock.GetPersonDynamicParameters(personModel), Times.Once());
        }

        [Fact]
        public void GetPersonById_ReturnsPersonModel()
        {
            var personRepositoryMock = new Mock<ISqlPersonRepository>();
            
            int personId = 72;
            string firstName = "sada";

            var expectedPerson = new PersonModel { Id = personId , FirstName = firstName};

            personRepositoryMock.Setup(mock => mock.GetPersonById(personId)).Returns(expectedPerson);

            var result = personRepositoryMock.Object.GetPersonById(personId);

            Assert.NotNull(result);
            Assert.IsType<PersonModel>(result);
            Assert.Equal(expectedPerson.Id, result.Id );
        }

        [Fact]
        public void GetPersonById_ReturnsPersonModel_Services()
        {
            var personRepositoryMock = new Mock<ISqlPersonService>();

            int personId = 72;
            string firstName = "sada";

            var expectedPerson = new PersonModel { Id = personId, FirstName = firstName };

            personRepositoryMock.Setup(mock => mock.GetPersonById(personId)).Returns(expectedPerson);

            var result = personRepositoryMock.Object.GetPersonById(personId);

            Assert.NotNull(result);
            Assert.IsType<PersonModel>(result);
            Assert.Equal(expectedPerson.Id, result.Id);
        }
    }
}