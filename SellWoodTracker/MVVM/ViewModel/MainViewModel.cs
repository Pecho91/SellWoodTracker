using SellWoodTracker.DataAccess;
using SellWoodTracker.MVVM.Commands;
using SellWoodTracker.MVVM.DataLoading;
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
using SellWoodTracker.Common.Model;

namespace SellWoodTracker.MVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly MainViewModelSqlDataLoading _mainViewModelSqlDataLoading;
        private readonly MainViewModelCommands _mainViewModelCommands;

        public ObservableCollection<PersonModel> RequestedPeople => _mainViewModelSqlDataLoading.RequestedPeople;
        public ObservableCollection<PersonModel> CompletedPeople => _mainViewModelSqlDataLoading.CompletedPeople;

        //public ICommand SomeCommand => _mainViewModelCommands.SomeCommand;

        public MainViewModel(MainViewModelSqlDataLoading mainViewModelSqlDataLoading)
        {
            _mainViewModelSqlDataLoading = mainViewModelSqlDataLoading;
            _mainViewModelSqlDataLoading.LoadDataFromSql();
        }
        

        
    }
}
