using Moq;
using SellWoodTracker.DataAccess.SqlConnectionFactorys;
using SellWoodTracker.GlobalConfig;

namespace SellWoodTracker.Tests.SqlConnectionFactoryTests
{
    public class SqlConnectionFactoryTests
    {
   
        [Fact]
        public void CreateSqlConnection_ShouldReturnNotNullConnection()
        {
            // Arrange
            var globalConfigMock = new Mock<IGlobalConfiguration>();
            var connectionString = "Server=DESKTOP-4ORQH0K;Database=SellWoodTracker;Trusted_Connection=True;";
            globalConfigMock.Setup(x => x.CnnString("SellWoodTracker")).Returns(connectionString);
          
            var sqlConnectionFactory = new SqlConnectionFactory(globalConfigMock.Object);

            // Act      
            var connection = sqlConnectionFactory.CreateSqlConnection();

            // Assert
            Assert.NotNull(connection);        
        }

        [Fact]
        public void CreateSqlConnection_ShouldReturnConnectionType()
        {
            // Arrange
            var globalConfigMock = new Mock<IGlobalConfiguration>();
            var connectionString = "Server=DESKTOP-4ORQH0K;Database=SellWoodTracker;Trusted_Connection=True;";
            globalConfigMock.Setup(x => x.CnnString("SellWoodTracker")).Returns(connectionString);

            var sqlConnectionFactory = new SqlConnectionFactory(globalConfigMock.Object);

            // Act      
            var connection = sqlConnectionFactory.CreateSqlConnection();

            // Assert  
            Assert.IsType<System.Data.SqlClient.SqlConnection>(connection);
        }

        [Fact]
        public void CreateSqlConnection_ShouldHaveCorrectConnectionString()
        {
            // Arrange
            var globalConfigMock = new Mock<IGlobalConfiguration>();
            var connectionString = "Server=DESKTOP-4ORQH0K;Database=SellWoodTracker;Trusted_Connection=True;";
            globalConfigMock.Setup(x => x.CnnString("SellWoodTracker")).Returns(connectionString);

            var expectedConnectionString = "Server=DESKTOP-4ORQH0K;Database=SellWoodTracker;Trusted_Connection=True;";

            var sqlConnectionFactory = new SqlConnectionFactory(globalConfigMock.Object);

            // Act      
            var connection = sqlConnectionFactory.CreateSqlConnection();

            // Assert
            Assert.Equal(expectedConnectionString, connectionString);
            Assert.Equal(expectedConnectionString, connection.ConnectionString);
        }

        [Fact]
        public void Constructor_ShouldThrowArgumentNullException_WhenGlobalConfigurationIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SqlConnectionFactory(null));
        }

        [Fact]
        public void CreateSqlConnection_ShouldHandleEmptyConnectionString()
        {
            // Arrange
            var globalConfigMock = new Mock<IGlobalConfiguration>();
            //var connectionString = "Server=DESKTOP-4ORQH0K;Database=SellWoodTracker;Trusted_Connection=True;";
            globalConfigMock.Setup(x => x.CnnString("SellWoodTracker")).Returns(string.Empty);

            var sqlConnectionFactory = new SqlConnectionFactory(globalConfigMock.Object);

            // Act
            var connection = sqlConnectionFactory.CreateSqlConnection();

            // Assert
            // Ensure that the connection is not null (even with an empty connection string).
            Assert.NotNull(connection);
        }

    }
}
