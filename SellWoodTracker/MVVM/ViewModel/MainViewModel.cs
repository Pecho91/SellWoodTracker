using SellWoodTracker.MVVM.Core;
using SellWoodTracker.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.ViewModel
{
    public class MainViewModel
    {
        public ICommand OpenAddPersonWindowCommand { get; }
        public MainViewModel()
        {
            OpenAddPersonWindowCommand = new RelayCommand(OpenAddPersonWindow);
        }

        private void OpenAddPersonWindow(object parameter)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.Show();
        }
    }
}
