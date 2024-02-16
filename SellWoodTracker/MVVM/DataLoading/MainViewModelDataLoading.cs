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

namespace SellWoodTracker.MVVM.DataLoading
{
    public class MainViewModelDataLoading : BaseViewModel
    {
        
        private readonly SqlDataOperations _connection;
        private ObservableCollection<PersonModel>? _requestedPeople;
        public ObservableCollection<PersonModel> RequestedPeople
        {
            get => _requestedPeople;
            set
            {
                _requestedPeople = value;
                OnPropertyChanged(nameof(RequestedPeople));
            }
        }

        private ObservableCollection<PersonModel>? _completedPeople;
        public ObservableCollection<PersonModel> CompletedPeople
        {
            get => _completedPeople;
            set
            {
                _completedPeople = value;
                OnPropertyChanged(nameof(CompletedPeople));
            }
        }

       

        private decimal _totalCompletedGrossIncome;
        public decimal TotalCompletedGrossIncome
        {
            get { return _totalCompletedGrossIncome; }
            set
            {
                _totalCompletedGrossIncome = Math.Round(value, 2);
                OnPropertyChanged(nameof(TotalCompletedGrossIncome));
            }
        }

        private decimal _totalCompletedMetricAmount;
        public decimal TotalCompletedMetricAmount
        {
            get { return _totalCompletedMetricAmount; }
            set
            {
                _totalCompletedMetricAmount = Math.Round(value, 2);
                OnPropertyChanged(nameof(TotalCompletedMetricAmount));
            }
        }

        public MainViewModelDataLoading(SqlDataOperations sqlDataConnections)
        {
            _sqlDataConnections = sqlDataConnections;
        }

        private void LoadDataFromSql()
        {
            if (_sqlConnection != null)
            {
                try
                {
                    List<PersonModel> requestedSqlPeople = _sqlConnection.GetRequestedPeople_All();
                    List<PersonModel> completedSqlPeople = _sqlConnection.GetCompletedPeople_All();

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
            else
            {
                Debug.WriteLine("SQL connection is not initialized.(null)");
                return;
            }
        }

        private void LoadDataFromExcel()
        {

            //if (_excelConnection != null)
            //{
            //    try
            //    {
            //        List<PersonModel> requestedExcelPeople = _excelConnection.GetRequestedPeople_All();
            //        List<PersonModel> completedExcelPeople = _excelConnection.GetCompletedPeople_All();

            //        RequestedPeople = new ObservableCollection<PersonModel>(requestedExcelPeople);
            //        CompletedPeople = new ObservableCollection<PersonModel>(completedExcelPeople);

            //        OnPropertyChanged(nameof(RequestedPeople));
            //        OnPropertyChanged(nameof(CompletedPeople));
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.WriteLine($"Error loading data from Excel: {ex.Message}");
            //    }
            //}
            //else
            //{
            //    Debug.WriteLine("Excel connection is not initialized.(null)");
            //    return;
            //}
        }
    }
}
