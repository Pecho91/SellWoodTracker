using SellWoodTracker.MVVM.DataLoading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.Commands
{
    public class AddPersonModelCommands 
    {
        
        public ICommand  MovePersonToCompletedCommand { get; set; }
        public ICommand DeletePersonFromRequested { get; set; }
        public ICommand DeletePersonFromCompleted { get; set; }
        public ICommand OpenAddPersonWindowCommand { get; set; }

    }

    public class MovePersonToCompletedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }

    // TODO
    public class MovePersonToCompletedCommand : ICommand
    {
        private readonly SqlDataAccessService _dataAccessService;

        public MovePersonToCompletedCommand(SqlDataAccessService dataAccessService)
        {
            _dataAccessService = dataAccessService;
        }

        public bool CanExecute(object parameter)
        {
            // Implement your logic here (e.g., check if a person can be moved)
            return true;
        }

        public void Execute(object parameter)
        {
            // Implement the logic to move a person to the completed list
            // Example: _dataAccessService.MovePersonToCompleted((Person)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
    public class MainViewModel1 : INotifyPropertyChanged
    {
        private readonly MovePersonToCompletedCommand _movePersonToCompletedCommand;
        private readonly SqlDataAccessService _dataAccessService;

        public MainViewModel1(
            MovePersonToCompletedCommand movePersonToCompletedCommand,
            SqlDataAccessService dataAccessService)
        {
            _movePersonToCompletedCommand = movePersonToCompletedCommand;
            _dataAccessService = dataAccessService;

            // Initialize other properties...
        }

        // Other view model logic...
    }
}
