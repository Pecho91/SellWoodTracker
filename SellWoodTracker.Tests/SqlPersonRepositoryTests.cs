

using Dapper;
using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlConnectionExecutor;
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

        [Fact]
        public void CreatePerson_Should_Call_Repository_Method()
        {
            // Arrange
            var model = new PersonModel { /* Initialize your model properties */ };
            var repositoryMock = new Mock<ISqlPersonRepository>();
            repositoryMock.Setup(repo => repo.CreatePerson(model));

            // Act
            repositoryMock.Object.CreatePerson(model);

            // Assert
            repositoryMock.Verify(repo => repo.CreatePerson(model), Times.Once);
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
                // Add more PersonModel objects as needed
             };

            var repositoryMock = new Mock<ISqlPersonRepository>();
            repositoryMock.Setup(repo => repo.GetRequestedPeople_All()).Returns(expectedPeople);

            // Act
            var actualPeople = repositoryMock.Object.GetRequestedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, actualPeople);
        }

        [Fact]
        public void GetRequestedPeople_All_Should_Return_List_Of_People1()
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
            };


            var connectionExecutorMock = new Mock<ISqlConnectionExecutor>();
            connectionExecutorMock.Setup(x => x.Execute(It.IsAny<Func<IDbConnection, List<PersonModel>>>()))
                                   .Returns(expectedPeople);

            // Mock ISqlDynamicParametersBuilder if needed
            var dynamicParametersBuilderMock = new Mock<ISqlDynamicParametersBuilder>();

            // Mock ISqlPersonRepository
            var repositoryMock = new Mock<ISqlPersonRepository>();
            repositoryMock.Setup(repo => repo.GetRequestedPeople_All()).Returns(expectedPeople);

            // Create SqlPersonRepository instance
            ISqlPersonRepository repository = repositoryMock.Object;

            // Act
            var actualPeople = repository.GetRequestedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, actualPeople);
        }
        
    }
}