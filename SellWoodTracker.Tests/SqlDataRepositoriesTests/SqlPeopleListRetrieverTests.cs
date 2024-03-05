using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlDataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests.SqlDataRepositoriesTests
{
    public class SqlPeopleListRetrieverTests
    {
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

                new PersonModel
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    EmailAddress = "jane.doe@example.com",
                    CellphoneNumber = "987654321",
                    DateTime = DateTime.Now,
                    MetricAmount = 20,
                    MetricPrice = 8
                },
             };

            var repositoryMock = new Mock<ISqlPeopleListRetriever>();
            repositoryMock.Setup(repo => repo.GetRequestedPeople_All()).Returns(expectedPeople);

            // Act
            var actualPeople = repositoryMock.Object.GetRequestedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, actualPeople);
        }

        [Fact]
        public void GetCompletedPeople_All_Should_Return_List_Of_People()
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

                new PersonModel
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    EmailAddress = "jane.doe@example.com",
                    CellphoneNumber = "987654321",
                    DateTime = DateTime.Now,
                    MetricAmount = 20,
                    MetricPrice = 8
                },
             };

            var repositoryMock = new Mock<ISqlPeopleListRetriever>();
            repositoryMock.Setup(repo => repo.GetCompletedPeople_All()).Returns(expectedPeople);

            // Act
            var actualPeople = repositoryMock.Object.GetCompletedPeople_All();

            // Assert
            Assert.Equal(expectedPeople, actualPeople);
        }
    }
}
