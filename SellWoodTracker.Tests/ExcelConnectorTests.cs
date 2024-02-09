using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Moq;
using ClosedXML.Excel;

namespace SellWoodTracker.Tests
{
    public class ExcelConnectorTests
    {   
        private readonly Mock<IGlobalConfig> _globalConfigMock;
        private readonly Mock<IDataConnection> _dataConnectionMock;
        private readonly ExcelConnector _excelConnector;
        private readonly string _testFilePath = "TestSellWoodTracker.xlsx";
       
        public ExcelConnectorTests()
        {
            _globalConfigMock = new Mock<IGlobalConfig>();
            _globalConfigMock.Setup(x => x.CnnString(It.IsAny<string>())).Returns(_testFilePath);

            //_dataConnectionMock = new Mock<IDataConnection>();
            //_dataConnectionMock.Setup(x => x.CreatePerson(It.IsAny<PersonModel>()));

            _excelConnector = new ExcelConnector(_globalConfigMock.Object);
        }
 
        [Fact]
        public void CreatePerson_Should_Save_To_Excel()
        {
            var roundedDateTime = DateTime.Now.Date;
          
            var person = new PersonModel
            {
                Id = 1,
                FirstName = "nameTest",
                LastName = "lastNameTest",
                CellphoneNumber = "123456789",
                EmailAddress = "mailTest@example.com",      
                DateTime = roundedDateTime,
                MetricAmount = 10m,
                MetricPrice = 20m,
                GrossIncome = 200m,
            };
            
            _excelConnector.CreatePerson(person);

            using (var workbook = new XLWorkbook(_testFilePath))
            {

                var worksheet = workbook.Worksheet("RequestedPeople");

                Assert.NotNull(worksheet);

                Assert.Equal(person.Id, worksheet.Cell(2, 1).GetValue<int>());
                Assert.Equal(person.FirstName, worksheet.Cell(2, 2).Value.ToString());
                Assert.Equal(person.LastName, worksheet.Cell(2, 3).Value.ToString());
                Assert.Equal(person.EmailAddress, worksheet.Cell(2, 4).Value.ToString());
                Assert.Equal(person.CellphoneNumber, worksheet.Cell(2, 5).Value.ToString());
                Assert.Equal(person.DateTime, worksheet.Cell(2, 6).GetDateTime().Date);
                Assert.Equal(person.MetricAmount, (worksheet.Cell(2, 7).GetValue<decimal>()));
                Assert.Equal(person.MetricPrice, (worksheet.Cell(2, 8).GetValue<decimal>()));
                Assert.Equal(person.GrossIncome, (worksheet.Cell(2, 9).GetValue<decimal>()));

            }

            //_dataConnectionMock.Verify(x => x.CreatePerson(It.IsAny<PersonModel>()), Times.Once);
        }
    }
}
