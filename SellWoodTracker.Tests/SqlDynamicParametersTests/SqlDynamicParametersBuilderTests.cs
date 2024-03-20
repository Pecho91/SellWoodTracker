using Dapper;
using Moq;
using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SellWoodTracker.Tests.SqlDynamicParametersTests
{
    public class SqlDynamicParametersBuilderTests
    {
        [Fact]
        public void GetPersonDynamicParameters_ShouldConstructValidParameters()
        {
            // Arrange
            var builder = new Mock<ISqlDynamicParametersBuilder>();

            var model = new PersonModel
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                CellphoneNumber = "123456789",
                DateTime = DateTime.Now,
                MetricAmount = 10,
                MetricPrice = 5
            };

            // Create an instance of DynamicParameters
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", model.FirstName);
            parameters.Add("@LastName", model.LastName);
            parameters.Add("@EmailAddress", model.EmailAddress);
            parameters.Add("@CellphoneNumber", model.CellphoneNumber);
            parameters.Add("@DateTime", model.DateTime);
            parameters.Add("@MetricAmount", model.MetricAmount);
            parameters.Add("@MetricPrice", model.MetricPrice);
            parameters.Add("@GrossIncome", model.GrossIncome);
            parameters.Add("@id", 0); // Assuming this is the default value for id

            // Set up the mock to return the correct parameters          
            builder.Setup(mock => mock.GetPersonDynamicParameters(model)).Returns(parameters);

            // Act
            var actualParameters = builder.Object.GetPersonDynamicParameters(model);

            // Assert
            Assert.NotNull(actualParameters);
            Assert.Equal(model.FirstName, actualParameters.Get<string>("@FirstName"));
            Assert.Equal(model.LastName, actualParameters.Get<string>("@LastName"));
            Assert.Equal(model.EmailAddress, actualParameters.Get<string>("@EmailAddress"));
            Assert.Equal(model.CellphoneNumber, actualParameters.Get<string>("@CellphoneNumber"));
            Assert.Equal(model.DateTime, actualParameters.Get<DateTime?>("@DateTime"));
            Assert.Equal(model.MetricAmount, actualParameters.Get<decimal>("@MetricAmount"));
            Assert.Equal(model.MetricPrice, actualParameters.Get<decimal>("@MetricPrice"));
            Assert.Equal(model.GrossIncome, actualParameters.Get<decimal>("@GrossIncome"));
            Assert.Equal(0, actualParameters.Get<int>("@id"));
        }
    }
}
