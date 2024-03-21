using SellWoodTracker.DataAccess.SqlDataInterfaces;
using SellWoodTracker.DataAccess.SqlDataRepositories;
using SellWoodTracker.MVVM.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.Commands
{
    
    public class OpenAddPersonWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public AddPersonWindow addPersonWindow;

        public OpenAddPersonWindowCommand() { 

           
        }

        public bool CanExecute(object parameter)
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);

            return true;
        }

        public void Execute(object parameter)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.Show();
            Debug.WriteLine("add clicked");
        }

    }
}
