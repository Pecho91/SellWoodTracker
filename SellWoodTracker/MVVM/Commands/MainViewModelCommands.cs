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

namespace SellWoodTracker.MVVM.Commands
{
    public class MainViewModelCommands : BaseViewModel
    {
        private readonly MainViewModelDataLoading _mainViewModelDataLoading;
        public ICommand MovePersonToCompletedCommand { get; }
        public ICommand DeletePersonFromRequestedCommand { get; }
        public ICommand DeletePersonFromCompletedCommand { get; }
        public ICommand OpenAddPersonWindowCommand { get; }


        private PersonModel? _selectedRequestedPerson;
        public PersonModel SelectedRequestedPerson
        {
            get { return _selectedRequestedPerson; }
            set
            {
                _selectedRequestedPerson = value;
                OnPropertyChanged(nameof(SelectedRequestedPerson));
            }
        }

        private PersonModel? _selectedCompletedPerson;
        public PersonModel SelectedCompletedPerson
        {
            get { return _selectedCompletedPerson; }
            set
            {
                _selectedCompletedPerson = value;
                OnPropertyChanged(nameof(SelectedCompletedPerson));
            }
        }

        public MainViewModelCommands()
        {
            _mainViewModelDataLoading = new MainViewModelDataLoading();

            MovePersonToCompletedCommand = new RelayCommand(MovePersonToCompletedDataGrid);
            DeletePersonFromRequestedCommand = new RelayCommand(DeletePersonFromRequestedDataGrid);
            DeletePersonFromCompletedCommand = new RelayCommand(DeletePersonFromCompletedDataGrid);
            OpenAddPersonWindowCommand = new RelayCommand(OpenAddPersonWindow);
        }

        private void MovePersonToCompletedDataGrid(object parameter)
        {
            if (SelectedRequestedPerson != null)
            {
                bool confirmed = ShowCompleteConfirmationDialog();

                if (confirmed)
                {

                    _mainViewModelDataLoading.MoveRequestedPersonToCompleted(SelectedCompletedPerson.Id);

                    //if (_sqlConnection != null)
                    //{
                    //    _sqlConnection.MoveRequestedPersonToCompleted(SelectedRequestedPerson.Id);
                    //}

                    //if (_excelConnection != null)
                    //{
                    //    _excelConnection.MoveRequestedPersonToCompleted(SelectedRequestedPerson.Id);
                    //}

                    Mediator.NotifyRefreshDataGrids();

                    Debug.WriteLine("move requested clicked");
                }
            }
        }

        private void DeletePersonFromRequestedDataGrid(object parameter)
        {
            if (SelectedRequestedPerson != null)
            {
                bool confirmed = ShowDeleteConfirmationDialog();

                if (confirmed)
                {
                    if (_sqlConnection != null)
                    {
                        _sqlConnection.DeletePersonFromRequested(SelectedRequestedPerson.Id);
                    }

                    if (_excelConnection != null)
                    {
                        _excelConnection.DeletePersonFromRequested(SelectedRequestedPerson.Id);
                    }

                    Mediator.NotifyRefreshDataGrids();

                    Debug.WriteLine("delete requested clicked");
                }
            }
        }

        private void DeletePersonFromCompletedDataGrid(object parameter)
        {
            if (SelectedCompletedPerson != null)
            {
                bool confirmed = ShowDeleteConfirmationDialog();

                if (confirmed)
                {
                    if (_sqlConnection != null)
                    {
                        _sqlConnection.DeletePersonFromCompleted(SelectedCompletedPerson.Id);
                    }

                    if (_excelConnection != null)
                    {
                        _excelConnection.DeletePersonFromCompleted(SelectedCompletedPerson.Id);
                    }

                    Mediator.NotifyRefreshDataGrids();

                    Debug.WriteLine("delete completed clicked");
                }
            }
        }

        private void RefreshPeopleInDataGrids(object? sender, EventArgs e)
        {
            if (_sqlConnection != null)
            {
                LoadDataFromSql();
            }
            if (_excelConnection != null)
            {
                LoadDataFromExcel();
            }
        }

        private void RefreshTotalEarn(object? sender, EventArgs e)
        {
            if (_sqlConnection != null)
            {
                UpdateTotalGrossIncome();
                UpdateTotalMetricAmount();
            }

            if (_excelConnection != null)
            {
                UpdateTotalGrossIncome();
                UpdateTotalMetricAmount();
            }

        }
        private void OpenAddPersonWindow(object parameter)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            addPersonWindow.Show();
            Debug.WriteLine("add clicked");
        }

        private void RefreshPeopleInDataGrids(object? sender, EventArgs e)
        {
            if (_sqlConnection != null)
            {
                LoadDataFromSql();
            }
            if (_excelConnection != null)
            {
                LoadDataFromExcel();
            }
        }

        private void RefreshTotalEarn(object? sender, EventArgs e)
        {
            if (_sqlConnection != null)
            {
                UpdateTotalGrossIncome();
                UpdateTotalMetricAmount();
            }

            if (_excelConnection != null)
            {
                UpdateTotalGrossIncome();
                UpdateTotalMetricAmount();
            }

        }
       
        private bool ShowDeleteConfirmationDialog()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        private bool ShowCompleteConfirmationDialog()
        {
            MessageBoxResult result = MessageBox.Show("Is it completed?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
    }
}

