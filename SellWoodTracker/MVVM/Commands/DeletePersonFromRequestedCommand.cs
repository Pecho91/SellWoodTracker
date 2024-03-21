using SellWoodTracker.DataAccess.SqlDataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.Commands
{
    public class DeletePersonFromRequestedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ISqlRequestedPersonDeleter _sqlRequestedPersonDeleter;
        private readonly int _personId;

        public DeletePersonFromRequestedCommand(ISqlRequestedPersonDeleter sqlPersonDeleter, int personId)
        {
            _sqlRequestedPersonDeleter = sqlPersonDeleter ?? throw new ArgumentNullException(nameof(sqlPersonDeleter));
            _personId = personId;
        }
            
        public bool CanExecute(object? parameter)
        {
            
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public void Execute(object? parameter)
        {
            _sqlRequestedPersonDeleter.DeletePersonFromRequested(_personId);
        }
    }
}   
