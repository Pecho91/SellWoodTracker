using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Commands;
using SellWoodTracker.MVVM.DataLoading;
using SellWoodTracker.MVVM.Core;
using SellWoodTracker.MVVM.DataLoading;
using SellWoodTracker.MVVM.Model;
using SellWoodTracker.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SellWoodTracker.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly MainViewModelDataLoading _mainViewModelDataLoading;
        private readonly MainViewModelCommands _mainViewModelCommands;

        public ObservableCollection<PersonModel> RequestedPeople => _mainViewModelDataLoading.RequestedPeople;
        public ObservableCollection<PersonModel> CompletedPeople => _mainViewModelDataLoading.CompletedPeople;

        public ICommand SomeCommand => _mainViewModelCommands.SomeCommand;

        public MainViewModel()
        {
            _mainViewModelDataLoading = new MainViewModelDataLoading();
            _mainViewModelCommands = new MainViewModelCommands();
        }
        

        
    }
}
