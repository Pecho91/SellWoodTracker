using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Core;
using SellWoodTracker.MVVM.Model;
using SellWoodTracker.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly GlobalConfig _globalConfig;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand OpenAddPersonWindowCommand { get; }
        private ObservableCollection<PersonModel>? _requestedPeople;
        private ObservableCollection<PersonModel>? _completedPeople;
        public ObservableCollection<PersonModel> RequestedPeople
        {
            get { return _requestedPeople; }
                
            set
            {
                _requestedPeople = value;
                OnPropertyChanged(nameof(RequestedPeople));
            }
        }

        public ObservableCollection<PersonModel> CompletedPeople
        {
            get { return _completedPeople; }

            set
            {
                _completedPeople = value;
                OnPropertyChanged(nameof(CompletedPeople));
            }
        }

        private readonly IDataConnection? _sqlConnection;
        private readonly IDataConnection? _excelConnection;

        private PersonModel? _selectedRequestedPerson;
        public PersonModel SelectedRequestedPerson
        {
            get { return _selectedRequestedPerson; }
            set
            {
                _selectedRequestedPerson = value;
                OnPropertyChanged(nameof(SelectedRequestedPerson));
            }
        }

        private PersonModel? _selectedCompletedPerson;
        public PersonModel SelectedCompletedPerson
        {
            get { return _selectedCompletedPerson; }
            set
            {
                _selectedCompletedPerson = value;
                OnPropertyChanged(nameof(SelectedCompletedPerson));
            }
        }

        public ICommand MovePersonToCompletedCommand { get; set; }
        public ICommand DeletePersonFromRequestedCommand { get; set; }
        public ICommand DeletePersonFromCompletedCommand { get; set; }

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

        public MainViewModel()
        {
            _globalConfig = new GlobalConfig();

            switch (_globalConfig.ChosenDatabase)
            {
                case DatabaseType.Sql:
                    _globalConfig.InitializeConnections(DatabaseType.Sql);
                    _sqlConnection = _globalConfig.Connection;                  
                    break;                   

                case DatabaseType.ExcelFile:
                    _globalConfig.InitializeConnections(DatabaseType.ExcelFile);
                    _excelConnection = _globalConfig.Connection;                 
                    break;

                default:
                    Debug.WriteLine("Database not selected / no database");
                    break;
            }

            LoadDataFromSql();
            LoadDataFromExcel();

            UpdateTotalGrossIncome();
            UpdateTotalMetricAmount();

            MovePersonToCompletedCommand = new RelayCommand(MovePersonToCompletedDataGrid);
            DeletePersonFromRequestedCommand = new RelayCommand(DeletePersonFromRequestedDataGrid);
            DeletePersonFromCompletedCommand = new RelayCommand(DeletePersonFromCompletedDataGrid);
            OpenAddPersonWindowCommand = new RelayCommand(OpenAddPersonWindow);

            Mediator.RefreshDataGrids += RefreshPeopleInDataGrids;
            Mediator.RefreshTotalEarn += RefreshTotalEarn;
         
        }

        private void OpenAddPersonWindow(object parameter)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.Show();
            Debug.WriteLine("add clicked");
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

            if (_excelConnection != null)
            {
                try
                {
                    List<PersonModel> requestedExcelPeople = _excelConnection.GetRequestedPeople_All();
                    List<PersonModel> completedExcelPeople = _excelConnection.GetCompletedPeople_All();

                    RequestedPeople = new ObservableCollection<PersonModel>(requestedExcelPeople);
                    CompletedPeople = new ObservableCollection<PersonModel>(completedExcelPeople);

                    OnPropertyChanged(nameof(RequestedPeople));
                    OnPropertyChanged(nameof(CompletedPeople));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading data from Excel: {ex.Message}");
                }            
            }
            else
            {
                Debug.WriteLine("Excel connection is not initialized.(null)");
                return;
            }
        }
       

        private void RefreshPeopleInDataGrids(object? sender, EventArgs e)
        {
            if (_sqlConnection != null)
            {
                LoadDataFromSql();          
            }
            if (_excelConnection != null)
            {
                LoadDataFromExcel();                
            }         
        }

        private void RefreshTotalEarn(object? sender, EventArgs e)
        {
            if (_sqlConnection != null)
            {
                UpdateTotalGrossIncome();
                UpdateTotalMetricAmount();
            }

            if (_excelConnection != null)
            {
                UpdateTotalGrossIncome();
                UpdateTotalMetricAmount();
            }
           
        }

        private void MovePersonToCompletedDataGrid(object parameter)
        {
            if(SelectedRequestedPerson != null)
            {
                bool confirmed = ShowCompleteConfirmationDialog();

                if (confirmed)
                {
                    if (_sqlConnection != null)
                    {
                        _sqlConnection.MoveRequestedPersonToCompleted(SelectedRequestedPerson.Id);
                    }

                    if (_excelConnection != null)
                    {
                        _excelConnection.MoveRequestedPersonToCompleted(SelectedRequestedPerson.Id);
                    }

                    Mediator.NotifyRefreshDataGrids();
                    
                    Debug.WriteLine("move requested clicked"); 
                }
            }        
        }

        private void DeletePersonFromRequestedDataGrid(object parameter)
        {
            if(SelectedRequestedPerson != null)
            {
                bool confirmed = ShowDeleteConfirmationDialog();

                if (confirmed)
                {
                    if (_sqlConnection != null)
                    {
                        _sqlConnection.DeletePersonFromRequested(SelectedRequestedPerson.Id);
                    }

                    if (_excelConnection != null)
                    {
                        _excelConnection.DeletePersonFromRequested(SelectedRequestedPerson.Id);
                    }
                 
                    Mediator.NotifyRefreshDataGrids();

                    Debug.WriteLine("delete requested clicked"); 
                }
            }
        }

        private void DeletePersonFromCompletedDataGrid(object parameter)
        {
            if (SelectedCompletedPerson != null)
            {
                bool confirmed = ShowDeleteConfirmationDialog();

                if (confirmed)
                {
                    if (_sqlConnection != null)
                    {
                        _sqlConnection.DeletePersonFromCompleted(SelectedCompletedPerson.Id);
                    }

                    if (_excelConnection != null)
                    {
                        _excelConnection.DeletePersonFromCompleted(SelectedCompletedPerson.Id);
                    }
                    
                    Mediator.NotifyRefreshDataGrids();

                    Debug.WriteLine("delete completed clicked"); 
                }
            }
        }

        private bool ShowDeleteConfirmationDialog()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        private bool ShowCompleteConfirmationDialog()
        {
            MessageBoxResult result = MessageBox.Show("Is it completed?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private decimal TotalGrossIncomeFromCompleted()
        {
            if (_sqlConnection != null)
            {
                try
                {
                    return _sqlConnection.GetTotalGrossIncomeFromCompleted();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error calculating total MetricPrice from Sql: {ex.Message}");
                }
            }

            if (_excelConnection != null)
            {
                try
                {
                    return _excelConnection.GetTotalGrossIncomeFromCompleted();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error calculating total MetricPrice from Excel: {ex.Message}");
                }                
            }

            return 0;
        }

        private decimal TotalMetricAmountFromCompleted()
        {
            if (_sqlConnection != null)
            {
                try
                {
                    return _sqlConnection.GetTotalMetricAmountFromCompleted();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error calculating total MetricAmount from Sql: {ex.Message}");
                }
            }

            if (_excelConnection != null)
            {
                try
                {
                    return _excelConnection.GetTotalMetricAmountFromCompleted();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error calculating total MetricAmount from Excel: {ex.Message}");
                }
            }

            return 0;
        }
        private void UpdateTotalGrossIncome()
        {
            TotalCompletedGrossIncome = TotalGrossIncomeFromCompleted();
            
        }

        private void UpdateTotalMetricAmount()
        {
            TotalCompletedMetricAmount = TotalMetricAmountFromCompleted();
            
        }
    }
}
