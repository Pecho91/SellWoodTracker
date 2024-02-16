using SellWoodTracker.MVVM.DataLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.Commands
{
    public class AddPersonModelCommands
    {
        private readonly MainViewModelDataLoading _dataLoading;

        public ICommand MovePersonToCompletedCommand { get; }
        public ICommand DeletePersonFromRequested { get; }
        public ICommand DeletePersonFromCompleted { get; }
        public ICommand OpenAddPersonWindowCommand { get; }

    }
}
