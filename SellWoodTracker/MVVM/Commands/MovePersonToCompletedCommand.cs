using SellWoodTracker.DataAccess.SqlDataInterfaces;
using SellWoodTracker.DataAccess.SqlDataRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.Commands
{
    
    public class MovePersonToCompletedCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly ISqlPersonMover _sqlPersonMover;
        private readonly int _personId;

        public MovePersonToCompletedCommand(SqlPersonMover sqlPersonMover, int personId) { 

            _sqlPersonMover = sqlPersonMover ?? throw new ArgumentNullException(nameof(sqlPersonMover));
            _personId = personId;

        }

        public bool CanExecute(object parameter)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public void Execute(object parameter)
        {
            _sqlPersonMover.MoveRequestedPersonToCompleted(_personId);
        }
       
    }
}
