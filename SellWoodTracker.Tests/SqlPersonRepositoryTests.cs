

using Dapper;
using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlConnectionFactory;
using SellWoodTracker.DataAccess.SqlDataRepository;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using SellWoodTracker.GlobalConfig;
using SellWoodTracker.Services.SqlServices;
using System.Data;
using static Dapper.SqlMapper;


namespace SellWoodTracker.Tests
{
    public class SqlPersonRepositoryTests
    {
        
        public SqlPersonRepositoryTests()
        {
            
        }

        //[Fact]
        //public void Constructor_WithBothConnectionFactoryAndDynamicParametersBuilder_ShouldExecuteCorrectly()
        //{           
        //    var connectionFactoryMock = new Mock<DataAccess.SqlConnector.ISqlConnectionFactory>();
        //    var dynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();

        //    var repository = new SqlPersonRepository(connectionFactoryMock.Object, dynamicParametersBuilderMock.Object);

        //    Assert.NotNull(repository);
        //    Assert.Same(connectionFactoryMock.Object, repository.SqlConnectionFactory);
        //    Assert.Same(dynamicParametersBuilderMock.Object, repository.SqlDynamicParametersBuilder);
        //}
        //[Fact]
        //public void CreatePerson_WhenCalled_ShouldExecuteCorrectly()
        //{
        //    //TODO (ISqlPersonRepository) ?? (specific testing)
        //    var connectionFactoryMock = new Mock<DataAccess.SqlConnector.ISqlConnectionFactory>();
        //    var dynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();       

        //    var repository = new SqlPersonRepository(connectionFactoryMock.Object, dynamicParametersBuilderMock.Object);

        //    var personModel = new PersonModel
        //    {
        //        FirstName = "John",
        //        LastName = "Doe",
        //        CellphoneNumber = "123456789",
        //        EmailAddress = "john.doe@example.com",
        //       // DateTime = DateTime.Now, // Assuming DateTime property is required
        //        MetricAmount = 10,
        //        MetricPrice = 5
        //    };

        //    repository.CreatePerson(personModel);

        //   // connectionFactoryMock.Verify(mock => mock.CreateSqlConnection(), Times.Once());
        //    dynamicParametersBuilderMock.Verify(mock => mock.GetPersonDynamicParameters(personModel), Times.Once());
        //}

        //[Fact]
        //public void CreatePerson_ShouldSetAllProperties()
        //{
        //    // Arrange
           
        //    var connectionFactoryMock = new Mock<DataAccess.SqlConnector.ISqlConnectionFactory>();
            

        //    connectionFactoryMock.Setup(x => x.CreateSqlConnection()).Returns(connectionMock.Object);

           

        //    var personModel = new PersonModel
        //    {
        //        Id = 72, // Set Id property here
        //        FirstName = "John",
        //        LastName = "Doe",
        //        CellphoneNumber = "123456789",
        //        EmailAddress = "john.doe@example.com",
        //        MetricAmount = 10,
        //        MetricPrice = 5
        //    };

        //    // Act
        //    repository.CreatePerson(personModel);

        //    // Assert
        //    Assert.Equal(72, personModel.Id);
        //    Assert.Equal("John", personModel.FirstName);
        //    Assert.Equal("Doe", personModel.LastName);
        //    Assert.Equal("123456789", personModel.CellphoneNumber);
        //    Assert.Equal("john.doe@example.com", personModel.EmailAddress);
        //    Assert.Equal(10, personModel.MetricAmount);
        //    Assert.Equal(5, personModel.MetricPrice);
        //    // Add more assertions as needed
        //}

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

        //[Fact]
        //public void GetRequestedPeople_All_ReturnsListOfPeople()
        //{
        //    var personRepositoryMock = new Mock<ISqlPersonRepository>();

        //    var personModel = new PersonModel
        //    {
        //        Id = personId,
        //        FirstName = firstName,


        //    };

        //    personRepositoryMock.Setup(mock => mock.GetCompletedPeople_All()).Returns(personModel);
        //}




        // Services (new class)
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