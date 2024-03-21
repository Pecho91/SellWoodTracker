using SellWoodTracker.DataAccess.SqlDataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.Commands
{
    
    public class DeletePersonFromCompletedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ISqlCompletedPersonDeleter _sqlCompletedPersonDeleter;
        private readonly int _personId;

        public DeletePersonFromCompletedCommand(ISqlCompletedPersonDeleter sqlPersonDeleter, int personId)
        {
            _sqlCompletedPersonDeleter = sqlPersonDeleter ?? throw new ArgumentNullException(nameof(sqlPersonDeleter));
            _personId = personId;
        }

        public bool CanExecute(object? parameter)
        {

            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public void Execute(object? parameter)
        {
            _sqlCompletedPersonDeleter.DeletePersonFromCompleted(_personId);
        }
    }
}
