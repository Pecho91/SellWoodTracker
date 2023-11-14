using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Core;
using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.ViewModel
{
    public class AddPersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private PersonModel _newPerson;
        public PersonModel NewPerson
        {
            get
            {
                return _newPerson;
            }
            set
            {
                if (_newPerson != value)
                {
                    _newPerson = value;
                    OnPropertyChanged(nameof(NewPerson));
                }
            }
        }

        public ICommand AddPersonCommand { get; }

        public AddPersonViewModel()
        {
            AddPersonCommand = new RelayCommand(AddPerson);
            NewPerson = new PersonModel();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

   
        
        private void AddPerson(object parameter)
        {
            if (NewPerson != null)
            {
                // Here, you'd handle adding this person to the database or any other logic
                SqlConnector sqlConnector = new SqlConnector();
                sqlConnector.CreatePerson(NewPerson);
                // Clear the input fields after adding the person
                NewPerson = new PersonModel(); // Optionally reset the NewPerson object for a new entry
            }
        }

    }
}
