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

        private readonly SqlConnector _sqlConnector;

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

        public ICommand MovePersonToCompletedCommand { get; set; }

        private ObservableCollection<PersonModel> _refreshRequestedDataGrid;
        public ObservableCollection<PersonModel> RefreshRequestedDataGrid
        {
            get { return _refreshRequestedDataGrid; }
            set
            {
                _refreshRequestedDataGrid = value;
                OnPropertyChanged(nameof(RefreshRequestedDataGrid));
            }
        }

        private ObservableCollection<PersonModel> _refreshCompletedDataGrid;
        public ObservableCollection<PersonModel> RefreshCompletedDataGrid
        {
            get { return _refreshCompletedDataGrid; }
            set
            {
                _refreshCompletedDataGrid = value;
                OnPropertyChanged(nameof(RefreshCompletedDataGrid));
            }
        }

        public MainViewModel()
        {               
            _sqlConnector = new SqlConnector();           
            LoadPeopleToRequestedDataGrid();
            LoadPeopleToCompletedDataGrid();
            MovePersonToCompletedCommand = new RelayCommand(MovePersonToCompleted);
            OpenAddPersonWindowCommand = new RelayCommand(OpenAddPersonWindow);
        }

        private void OpenAddPersonWindow(object parameter)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.Show();
            Debug.WriteLine("add clicked");
        }

        private void LoadPeopleToRequestedDataGrid()
        {
            List<PersonModel> requestedPeople = _sqlConnector.GetRequestedPeople_All();          
            RequestedPeople = new ObservableCollection<PersonModel>(requestedPeople);
        }

        private void LoadPeopleToCompletedDataGrid()
        {
            List<PersonModel> completedPeople = _sqlConnector.GetCompletedPeople_All();
            CompletedPeople = new ObservableCollection<PersonModel>(completedPeople);
        }

        private void MovePersonToCompleted(object parameter)
        {
            if(SelectedRequestedPerson != null)
            {
                _sqlConnector.MoveRequestedPersonToCompleted(SelectedRequestedPerson.Id);

                LoadPeopleToRequestedDataGrid();
                LoadPeopleToCompletedDataGrid(); 
                
                OnPropertyChanged(nameof(RefreshRequestedDataGrid));
                OnPropertyChanged(nameof(RefreshCompletedDataGrid));
            }

            Debug.WriteLine("button move clicked");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
