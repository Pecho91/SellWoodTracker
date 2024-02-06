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

namespace SellWoodTracker.Tests
{
    public class ExcelConnectorTests
    {
        private readonly ExcelConnector _excelConnector;
       
        
     
        public ExcelConnectorTests(ITestOutputHelper output) 
        {
           // _excelConnector = new ExcelConnector();
        }

        [Fact]
        public void GetOrCreateWorkbook_FileExist_ReturnsWorkbook()
        {
            var globalConfigMock = new Mock<IGlobalConfig>();
            
        }
    }
}
