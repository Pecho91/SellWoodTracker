using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Core;
using SellWoodTracker.MVVM.Model;
using SellWoodTracker.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand OpenAddPersonWindowCommand { get; }
        private ObservableCollection<PersonModel> _requestedPeople;
        private ObservableCollection<PersonModel> _completedPeople;
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

        private readonly IDataConnection _sqlConnection;
        private readonly IDataConnection _excelConnection;

        private PersonModel _selectedRequestedPerson;
        public PersonModel SelectedRequestedPerson
        {
            get { return _selectedRequestedPerson; }
            set
            {
                _selectedRequestedPerson = value;
                OnPropertyChanged(nameof(SelectedRequestedPerson));
            }
        }

        private PersonModel _selectedCompletedPerson;
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
         
        public MainViewModel()
        {
            GlobalConfig.InitializeConnections(DatabaseType.Sql);
            _sqlConnection = GlobalConfig.Connection;

            GlobalConfig.InitializeConnections(DatabaseType.ExcelFile);
            _excelConnection = GlobalConfig.Connection;

            //LoadDataFromSql();
            LoadDataFromExcel();

            MovePersonToCompletedCommand = new RelayCommand(MovePersonToCompletedDataGrid);
            DeletePersonFromRequestedCommand = new RelayCommand(DeletePersonFromRequestedDataGrid);
            DeletePersonFromCompletedCommand = new RelayCommand(DeletePersonFromCompletedDataGrid);
            OpenAddPersonWindowCommand = new RelayCommand(OpenAddPersonWindow);

            Mediator.RefreshDataGrids += RefreshPeopleInDataGrids;

        }

        private void OpenAddPersonWindow(object parameter)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.Show();
            Debug.WriteLine("add clicked");
        }

        private void LoadDataFromSql()
        {
            List<PersonModel> requestedSqlPeople = _sqlConnection.GetRequestedPeople_All();
            RequestedPeople = new ObservableCollection<PersonModel>(requestedSqlPeople);
            OnPropertyChanged(nameof(RequestedPeople));

            List<PersonModel> completedSqlPeople = _sqlConnection.GetCompletedPeople_All();
            CompletedPeople = new ObservableCollection<PersonModel>(completedSqlPeople);
            OnPropertyChanged(nameof(CompletedPeople));
        }

        private void LoadDataFromExcel()
        {
            List<PersonModel> requestedExcelPeople = _excelConnection.GetRequestedPeople_All();
            RequestedPeople = new ObservableCollection<PersonModel>(requestedExcelPeople);
            OnPropertyChanged(nameof(RequestedPeople));

            List<PersonModel> completedExcelPeople = _excelConnection.GetCompletedPeople_All();
            CompletedPeople = new ObservableCollection<PersonModel>(completedExcelPeople);
            OnPropertyChanged(nameof(CompletedPeople));
        }
       

        private void RefreshPeopleInDataGrids(object? sender, EventArgs e)
        {

            //LoadDataFromSql();
            LoadDataFromExcel();
        }

        private void MovePersonToCompletedDataGrid(object parameter)
        {
            if(SelectedRequestedPerson != null)
            {
                _sqlConnection.MoveRequestedPersonToCompleted(SelectedRequestedPerson.Id);
                _excelConnection.MoveRequestedPersonToCompleted(SelectedRequestedPerson.Id);

                Mediator.NotifyRefreshDataGrids();

                Debug.WriteLine("move requested clicked");
            }        
        }

        private void DeletePersonFromRequestedDataGrid(object parameter)
        {
            if(SelectedRequestedPerson != null)
            {
                _sqlConnection.DeletePersonFromRequested(SelectedRequestedPerson.Id);
                _excelConnection.DeletePersonFromRequested(SelectedRequestedPerson.Id);

                Mediator.NotifyRefreshDataGrids();

                Debug.WriteLine("delete requested clicked");
            }
        }

        private void DeletePersonFromCompletedDataGrid(object parameter)
        {
            if (SelectedCompletedPerson != null)
            {
                _sqlConnection.DeletePersonFromCompleted(SelectedCompletedPerson.Id);
                _excelConnection.DeletePersonFromCompleted(SelectedCompletedPerson.Id);

                Mediator.NotifyRefreshDataGrids();

                Debug.WriteLine("delete completed clicked");
            }
        }


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
