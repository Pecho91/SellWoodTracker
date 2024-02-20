using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.Services
{
    public class DataAccessFactory : IDataAccessFactory
    {
        public IDataAccessFactory
    }

    //TODO
    //// MVVM Application
    //public class MyViewModel
    //{
    //    private readonly IDataService _dataService;

    //    public MyViewModel(IDataService dataService)
    //    {
    //        _dataService = dataService;
    //    }

    //    public async Task LoadData()
    //    {
    //        var data = await _dataService.GetData();
    //        // Process the data
    //    }
    //}

    //// Services Layer
    //public class DataService : IDataService
    //{
    //    private readonly IDataAccessLayer _dataAccessLayer;

    //    public DataService(IDataAccessLayer dataAccessLayer)
    //    {
    //        _dataAccessLayer = dataAccessLayer;
    //    }

    //    public async Task<IEnumerable<DataModel>> GetData()
    //    {
    //        // Fetch data from data access layer
    //        return await _dataAccessLayer.GetData();
    //    }
    //}
}
