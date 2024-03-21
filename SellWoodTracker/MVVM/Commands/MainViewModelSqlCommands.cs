using SellWoodTracker.DataAccess;
using SellWoodTracker.GlobalConfig;
using SellWoodTracker.MVVM.Core;
using SellWoodTracker.MVVM.View;
using SellWoodTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Data.SqlClient;
using SellWoodTracker.MVVM.DataLoading;
using SellWoodTracker.Common.Model;
using SellWoodTracker.Services.SqlServices;

namespace SellWoodTracker.MVVM.Commands
{
    public class MainViewModelSqlCommands 
    {
        
        public ICommand MovePersonToCompletedCommand { get; }
        public ICommand DeletePersonFromRequestedCommand { get; }
        public ICommand DeletePersonFromCompletedCommand { get; }
        public ICommand OpenAddPersonWindowCommand { get; }

    }
}

