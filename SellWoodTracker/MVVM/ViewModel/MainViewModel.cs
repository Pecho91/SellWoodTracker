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

        private decimal _totalCompletedMetricPrice;
        public decimal TotalCompletedMetricPrice
        {
            get { return _totalCompletedMetricPrice; }
            set
            {
                _totalCompletedMetricPrice = value;
                OnPropertyChanged(nameof(TotalCompletedMetricPrice));
            }
                

                
        }
        
        public MainViewModel()
        {                    
            switch (GlobalConfig.ChosenDatabase)
            {
                case DatabaseType.Sql:
                    GlobalConfig.InitializeConnections(DatabaseType.Sql);
                    _sqlConnection = GlobalConfig.Connection;                  
                    break;                   

                case DatabaseType.ExcelFile:
                    GlobalConfig.InitializeConnections(DatabaseType.ExcelFile);
                    _excelConnection = GlobalConfig.Connection;                 
                    break;

                default:
                    Debug.WriteLine("Database not selected / no database");
                    break;
            }

            LoadDataFromSql();
            LoadDataFromExcel();

            UpdateTotalEarnMetricPrice();

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
                
            }

            if (_excelConnection != null)
            {
                UpdateTotalEarnMetricPrice();
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

        private decimal CalculatedTotalMetricPriceFromCompleted()
        {
            if (_excelConnection != null)
            {
                try
                {
                    return _excelConnection.GetTotalMetricPriceFromCompleted();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error calculating total MetricPrice from Excel: {ex.Message}");
                }                
            }

            return 0;
        }

        private void UpdateTotalEarnMetricPrice()
        {
            TotalCompletedMetricPrice = CalculatedTotalMetricPriceFromCompleted();
            
        }
    }
}
