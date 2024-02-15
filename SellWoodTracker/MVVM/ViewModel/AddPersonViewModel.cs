using SellWoodTracker.MVVM.Core;
using SellWoodTracker.MVVM.Model;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.ViewModel
{
    public class AddPersonViewModel : INotifyPropertyChanged
    {
        private readonly GlobalConfig _globalConfig;
        public event PropertyChangedEventHandler? PropertyChanged;

        private MainViewModel _mainViewModel;
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
        public ICommand ClearFieldsCommand { get; }

        
        public AddPersonViewModel()
        {
            _globalConfig = new GlobalConfig();
            AddPersonCommand = new RelayCommand(AddPerson);
            ClearFieldsCommand = new RelayCommand(ClearFields);
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
                                       
                switch (_globalConfig.ChosenDatabase)
                {
                    case DatabaseType.ExcelFile:
                        _globalConfig.Connection?.CreatePerson(NewPerson); 
                        break;

                    case DatabaseType.Sql:
                        _globalConfig.Connection?.CreatePerson(NewPerson); 
                        break;

                    default:
                        Debug.WriteLine("Database not selected / no database");
                        break;
                }

                NewPerson = new PersonModel(); // Optionally reset the NewPerson object for a new entry

                Mediator.NotifyRefreshDataGrids();
                Debug.WriteLine("addButton click");
            }
        }

        private void ClearFields(object parameter)
        {
            NewPerson = new PersonModel();
        }

        
    }
}
