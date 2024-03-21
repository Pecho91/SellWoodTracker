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

        private readonly ISqlPersonDeleter _sqlPersonDeleter;
        private readonly int _personId;

        public DeletePersonFromRequestedCommand(ISqlPersonDeleter sqlPersonDeleter, int personId)
        {
            _sqlPersonDeleter = sqlPersonDeleter ?? throw new ArgumentNullException(nameof(sqlPersonDeleter));
            _personId = personId;
        }
            
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _sqlPersonDeleter.DeletePersonFromRequested(_personId);
        }
    }
}   // TODO other classes like that.
