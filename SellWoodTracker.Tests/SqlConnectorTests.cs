

using Moq;
using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Model;

namespace SellWoodTracker.Tests
{
    //public class SqlConnectorTests
    //{
    //    private readonly Mock<IGlobalConfig> _globalConfigMock;
    //    private SqlConnector _sqlConnector;

    //    public SqlConnectorTests()
    //    {
    //        _globalConfigMock = new Mock<IGlobalConfig>();
    //        _globalConfigMock.Setup(x => x.CnnString(It.IsAny<string>())).Returns("MockConnectionString");

    //        _sqlConnector = new SqlConnector(_globalConfigMock.Object);
    //    }


    //    [Fact]
    //    public void CreatePerson_Should_Call_CreatePerson_On_DataConnection()
    //    {
    //        var personModel = new PersonModel
    //        {
    //            FirstName = "Test",
    //            LastName = "Person",
    //        };

    //        // personModel.DateTime = new DateTime(2022, 1, 1);

    //        _sqlConnector.CreatePerson(personModel);

    //        //_mockDataConnection.Verify(dc => dc.CreatePerson(It.Is<PersonModel>(pm => pm.FirstName == "Test" && pm.LastName == "Person" && pm.DateTime == new DateTime(2022, 1, 1))), Times.Once());
    //    }
    //}
}