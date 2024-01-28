using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests
{
    public class ExcelConnectorTests
    {
        private ExcelConnector _excelConnector;
        private readonly string _testExcelFilePath = "C:/Users/andri/OneDrive/Documents/SellWoodTracker.xlsx";
        public string testFilePath = GlobalConfig.CnnString("SellWoodTracker.xlsx");

        public ExcelConnectorTests() 
        {
            
            _excelConnector = new ExcelConnector();
            
        }

        [Fact]
        public void CreatePerson_Should_Add_Person_To_RequestedPeople()
        {   
            
            var personModel = new PersonModel
            {
                FirstName = "Test",
                LastName = "Person",
            };

            _excelConnector.CreatePerson(personModel);

            var requestedPeople = _excelConnector.GetRequestedPeople_All();
            foreach (var person in requestedPeople)
            {
                Debug.WriteLine($"Id: {person.Id}, FirstName: {person.FirstName}, LastName: {person.LastName}");
            }

        
            Assert.Single(requestedPeople);
            Assert.Equal("Test", requestedPeople[0].FirstName);
            Assert.Equal("Person", requestedPeople[1].LastName);
        }
    }
}
