﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellWoodTracker.MVVM.ViewModel
{
    public class Mediator
    {
        public static event EventHandler? RefreshDataGrids;
        public static event EventHandler? RefreshTotalEarn;

        public static void NotifyRefreshDataGrids()
        {
            RefreshDataGrids?.Invoke(null, EventArgs.Empty);
            RefreshTotalEarn?.Invoke(null, EventArgs.Empty);
        }
    }
}
