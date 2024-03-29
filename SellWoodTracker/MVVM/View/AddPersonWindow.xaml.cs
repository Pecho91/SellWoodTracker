﻿using SellWoodTracker.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SellWoodTracker.MVVM.View
{
    /// <summary>
    /// Interaction logic for AddPersonWindow.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        public AddPersonWindow()
        {
            InitializeComponent();
            Loaded += AddPersonWindow_Loaded;
        }

        private void AddPersonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new AddPersonViewModel();
        }
    }
}
