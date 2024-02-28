﻿using Moq;
using SellWoodTracker.DataAccess.SqlDataRepository;
using SellWoodTracker.GlobalConfig;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Tests
{
    public class SqlConnectionFactoryTests
    {
        [Fact]
        public void CreateSqlConnection_ShouldReturnSqlConnection()
        {
            var globalConfigMock = new Mock<IGlobalConfiguration>();
            
            var connectionString = "Server=DESKTOP-4ORQH0K;Database=SellWoodTracker;Trusted_Connection=True;";

            globalConfigMock.Setup(x => x.CnnString("SellWoodTracker")).Returns(connectionString);

            var connectionFactory = new SqlConnectionFactory(globalConfigMock.Object);

            var connection = connectionFactory.CreateSqlConnection();

            Assert.NotNull(connection);
            Assert.IsType<System.Data.SqlClient.SqlConnection>(connection);
            Assert.Equal(connectionString, connection.ConnectionString);
        }
    }
}
