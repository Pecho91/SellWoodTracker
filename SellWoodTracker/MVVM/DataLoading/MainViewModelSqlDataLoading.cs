using SellWoodTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SellWoodTracker.Common;
using SellWoodTracker.DataAccess.SqlDataAccess;
using SellWoodTracker.Common.Model;
using SellWoodTracker.Services.SqlServices;
using SellWoodTracker.DataAccess.SqlDataRepository;

namespace SellWoodTracker.MVVM.DataLoading
{
    public class MainViewModelSqlDataLoading : BaseViewModel
    {
        
        private readonly InitializeSqlConnection _initializeSqlConnection;
        private readonly SqlPersonService _sqlPersonService;

        private ObservableCollection<PersonModel> _requestedPeople;
        public ObservableCollection<PersonModel> RequestedPeople
        {
            get => _requestedPeople;
            set
            {
                _requestedPeople = value;
                OnPropertyChanged(nameof(RequestedPeople));
            }
        }

        private ObservableCollection<PersonModel> _completedPeople;
        public ObservableCollection<PersonModel> CompletedPeople
        {
            get => _completedPeople;
            set
            {
                _completedPeople = value;
                OnPropertyChanged(nameof(CompletedPeople));
            }
        }

       

        //private decimal _totalCompletedGrossIncome;
        //public decimal TotalCompletedGrossIncome
        //{
        //    get { return _totalCompletedGrossIncome; }
        //    set
        //    {
        //        _totalCompletedGrossIncome = Math.Round(value, 2);
        //        OnPropertyChanged(nameof(TotalCompletedGrossIncome));
        //    }
        //}

        //private decimal _totalCompletedMetricAmount;
        //public decimal TotalCompletedMetricAmount
        //{
        //    get { return _totalCompletedMetricAmount; }
        //    set
        //    {
        //        _totalCompletedMetricAmount = Math.Round(value, 2);
        //        OnPropertyChanged(nameof(TotalCompletedMetricAmount));
        //    }
        //}

        public MainViewModelSqlDataLoading()
        {
            //_initializeSqlConnection = new InitializeSqlConnection(repository); 
            //_sqlPersonService = _initializeSqlConnection;
            LoadDataFromSql();
        }

        public void LoadDataFromSql()
        {
            try
            {
                List<PersonModel> requestedSqlPeople = _sqlPersonService.GetRequestedPeople_All();
                List<PersonModel> completedSqlPeople = _sqlPersonService.GetCompletedPeople_All();

                RequestedPeople = new ObservableCollection<PersonModel>(requestedSqlPeople);
                CompletedPeople = new ObservableCollection<PersonModel>(completedSqlPeople);

                OnPropertyChanged(nameof(RequestedPeople));
                OnPropertyChanged(nameof(CompletedPeople));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data from Sql: {ex.Message}");
            }
        }
    }       
}
