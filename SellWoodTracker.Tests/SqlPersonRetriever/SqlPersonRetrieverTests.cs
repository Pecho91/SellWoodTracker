using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests.SqlPersonRetriever
{
    public class SqlPersonRetrieverTests
    {
        [Fact]
        public void GetPersonById_ReturnsPersonModel()
        {
            var personRepositoryMock = new Mock<ISqlPersonRetriever>();

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
