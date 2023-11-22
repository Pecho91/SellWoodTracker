using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Core;
using SellWoodTracker.MVVM.Model;
using SellWoodTracker.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
                _requestedPeople = value;
                OnPropertyChanged(nameof(CompletedPeople));
            }
        }
        private readonly SqlConnector _sqlConnector;
        
        public MainViewModel()
        {               
            _sqlConnector = new SqlConnector();           
            LoadPeopleToRequestedDataGrid();
            
            OpenAddPersonWindowCommand = new RelayCommand(OpenAddPersonWindow);
        }

        private void OpenAddPersonWindow(object parameter)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.Show();
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
