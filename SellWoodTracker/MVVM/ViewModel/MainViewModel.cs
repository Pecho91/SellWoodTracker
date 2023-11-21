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
        private ObservableCollection<PersonModel> _persons;
        public ObservableCollection<PersonModel> Persons
        {
            get { return _persons; }
                
            set
            {
                _persons = value;
                OnPropertyChanged(nameof(Persons));
            }
        }

        private readonly SqlConnector _sqlConnector;
        
        public MainViewModel()
        {               
            _sqlConnector = new SqlConnector();
            
            LoadPeopleToRequestedListBox();
            
            OpenAddPersonWindowCommand = new RelayCommand(OpenAddPersonWindow);
        }

        private void OpenAddPersonWindow(object parameter)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.Show();
        }

        private void LoadPeopleToRequestedListBox()
        {
            List<PersonModel> people = _sqlConnector.GetPerson_All();
           
            Persons = new ObservableCollection<PersonModel>(people);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
