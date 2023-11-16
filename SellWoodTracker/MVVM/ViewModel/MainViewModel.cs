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
        public ObservableCollection<PersonModel> Persons { get; set; }
        private readonly SqlConnector _sqlConnector;
        public MainViewModel()
        {
            
            OpenAddPersonWindowCommand = new RelayCommand(OpenAddPersonWindow);
            _sqlConnector = new SqlConnector();
            LoadPersonsToRequestedListBox();
        }

        private void OpenAddPersonWindow(object parameter)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.Show();
        }

        private void LoadPersonsToRequestedListBox()
        {
            Persons = new ObservableCollection<PersonModel>(_sqlConnector.GetPerson_All());
        }
    }
}
