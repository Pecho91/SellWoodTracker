using SellWoodTracker.Common.Model;
using SellWoodTracker.DataAccess.SqlDynamicParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests
{
    public class SqlDynamicParametersBuilderTests
    {
        [Fact]
        public void GetPersonDynamicParameters_ShouldConstructValidParameters()
        {
            // Arrange
            var builder = new SqlDynamicParametersBuilder();
            var model = new PersonModel
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                CellphoneNumber = "123456789",
                DateTime = System.DateTime.Now,
                MetricAmount = 10,
                MetricPrice = 5
            };

            // Act
            var parameters = builder.GetPersonDynamicParameters(model);

            // Assert
            Assert.NotNull(parameters);
            Assert.Equal(model.FirstName, parameters.Get<string>("@FirstName"));
            Assert.Equal(model.LastName, parameters.Get<string>("@LastName"));
            Assert.Equal(model.EmailAddress, parameters.Get<string>("@EmailAddress"));
            Assert.Equal(model.CellphoneNumber, parameters.Get<string>("@CellphoneNumber"));
            Assert.Equal(model.DateTime, parameters.Get<DateTime?>("@DateTime"));
            Assert.Equal(model.MetricAmount, parameters.Get<decimal>("@MetricAmount"));
            Assert.Equal(model.MetricPrice, parameters.Get<decimal>("@MetricPrice"));
            Assert.Equal(model.GrossIncome, parameters.Get<decimal>("@GrossIncome"));
            Assert.Equal(0, parameters.Get<int>("@id"));
        }
    }
}
