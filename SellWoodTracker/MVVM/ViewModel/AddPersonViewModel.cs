using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Core;
using SellWoodTracker.MVVM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.ViewModel
{
    public class AddPersonViewModel : INotifyPropertyChanged
    {
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
            AddPersonCommand = new RelayCommand(AddPerson);
            ClearFieldsCommand = new RelayCommand(ClearFields);
            NewPerson = new PersonModel();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }  
        
        //private void AddPerson(object parameter)
        //{
        //    if (NewPerson != null)
        //    {
        //        DatabaseType chosenDatabase = DatabaseType.Sql;

        //        switch (chosenDatabase)
        //        {
        //            case DatabaseType.ExcelFile:

        //                ExcelConnector excelConnector = new ExcelConnector();
        //                excelConnector.CreatePerson(NewPerson);

        //                NewPerson = new PersonModel(); // Optionally reset the NewPerson object for a new entry

        //                Mediator.NotifyRefreshDataGrids();
        //                Debug.WriteLine("addButton click");
        //                break;

        //            case DatabaseType.Sql:
        //                SqlConnector sqlConnector = new SqlConnector();
        //                sqlConnector.CreatePerson(NewPerson);

        //                NewPerson = new PersonModel(); // Optionally reset the NewPerson object for a new entry

        //                Mediator.NotifyRefreshDataGrids();
        //                Debug.WriteLine("addButton click");
        //                break;
                        
        //            default:
        //                Debug.WriteLine("Database not selected / no database");
        //                break;
        //        }                            
        //    }          
        //}

        private void AddPerson(object parameter)
        {
            if (NewPerson != null)
            {
                // Choose the database type here based on your conditions or user input
                DatabaseType chosenDatabase = DatabaseType.ExcelFile; // For example, defaulting to SQL here

                // Initialize connections based on chosen database
                GlobalConfig.InitializeConnections(chosenDatabase);

                // Create the person based on the selected database
                switch (chosenDatabase)
                {
                    case DatabaseType.ExcelFile:
                        GlobalConfig.Connection?.CreatePerson(NewPerson); // Call CreatePerson based on Excel connection
                        break;

                    case DatabaseType.Sql:
                        GlobalConfig.Connection?.CreatePerson(NewPerson); // Call CreatePerson based on SQL connection
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
