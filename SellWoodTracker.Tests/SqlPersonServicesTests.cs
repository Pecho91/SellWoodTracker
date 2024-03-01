using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.Services.SqlServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests
{
    public class SqlPersonServicesTests
    {
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
