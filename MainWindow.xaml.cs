﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
using RDB_Project.DataReading;
using RDB_Project.DataWriting;
using RDB_Project.Logging;
using EntityFramework.BulkInsert.Extensions;
using RDB_Project.View;

namespace RDB_Project
{
    //todo - vyřešit vlákna, aby nedocházelo k zatuhnutí okna při výpisu logu atd.
    //todo - centrování textu tlačítek - windows > 7 jsou vlevo
    
    internal class RdbException : ApplicationException
    {
        public RdbException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ReaderFactory _readerFactory;
        private int _itemsPerPage = 10;
        private Stopwatch timer = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
            buttonNext.Visibility = Visibility.Hidden;
            buttonBack.Visibility = Visibility.Hidden;
            MessageBlock.Text = "";
            TimeBlock.Text = "";
            StatusProgress.IsIndeterminate = false;
        }

        private void _Add(object sender, RoutedEventArgs e)
        {
            AddDialog dialog = new AddDialog();
            dialog.Show();
        }

        /// <summary>
        /// Výpis logu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Log(object sender, RoutedEventArgs e)
        {
            try
            {
                MainGrid.Children.Add(element: Logging.LogGrid.CreateGrid(Log.GetData()));
            }
            catch (RdbException mes)
            {
                MessageBox.Show(mes.Message,"Chyba!",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            
        }

        private async void btn_search_Click(object sender, RoutedEventArgs e)
        {
            StatusProgress.Value = 0;
            StatusProgress.IsIndeterminate = true;
            
            SearchInput argumentsResult = new SearchInput();
            if(dateFrom.SelectedDate.HasValue)
                argumentsResult.DateFrom = dateFrom.SelectedDate.Value;
            if(dateTo.SelectedDate.HasValue)
                argumentsResult.DateTo = dateTo.SelectedDate.Value;

            TimeSpan timeFromSpan;
            bool boolTimeFrom = TimeSpan.TryParse(timeFrom.Text, out timeFromSpan);
            argumentsResult.TimeFrom = boolTimeFrom ? timeFromSpan : new TimeSpan(0, 0, 0);

            TimeSpan timeToSpan;
            bool boolTimeTo = TimeSpan.TryParse(timeTo.Text, out timeToSpan);
            argumentsResult.TimeTo = boolTimeTo ? timeToSpan : new TimeSpan(0,0,0);

            double x;
            bool boolX = double.TryParse(placeX.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out x);
            argumentsResult.x = boolX ? x : -1;

            double y;
            bool boolY = double.TryParse(placeY.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out y);
            argumentsResult.y = boolY ? y : -1;

            double varianceD;
            bool boolVariance = double.TryParse(variance.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out varianceD);
            argumentsResult.difference = boolVariance ? varianceD : -1;

            argumentsResult.serialNumber = device.Text;

            timer.Start();
            _readerFactory = ReaderFactory.CreateFactory(argumentsResult, _itemsPerPage);
            await _readerFactory.Prepare();
            timer.Stop();

            try
            {
                UpdateGrid();
            }
            catch (RdbException v)
            {
                MainGrid.Children.Clear();
                MessageBox.Show(v.Message);
            }
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            _readerFactory.NextPage();
            UpdateGrid();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            _readerFactory.PreviousPage();
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            DisplayingButtons();
            List<SearchResult> data = _readerFactory.GetResults().ToList();

            DataGrid searchGrid = View.SearchGrid.CreateGrid(data);
            MainGrid.Children.Add(element: searchGrid);
            
            MessageBlock.Text = string.Format("Nalezeno celkem: {0} záznamů", _readerFactory.TotalRecords);
            TimeBlock.Text = "Čas dotazu: "+timer.Elapsed.ToString();
            TextBlock.Text = string.Format("Stránka {0} z {1}", _readerFactory.Paginator.CurrentPage,
                _readerFactory.Paginator.TotalPages);
            StatusProgress.IsIndeterminate = false;
            StatusProgress.Value = 100;
        }

        /// <summary>
        /// Stará se o zobrazování stránkovacích tlačítek
        /// </summary>
        private void DisplayingButtons()
        {
            if (_readerFactory.IsFirstPage)
                buttonBack.Visibility = Visibility.Hidden;
            else buttonBack.Visibility = Visibility.Visible;

            if (_readerFactory.IsLastPage)
                buttonNext.Visibility = Visibility.Hidden;
            else buttonNext.Visibility = Visibility.Visible;
        }

        private void _Export(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV File (*.csv)|*.csv";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string jmeno = dialog.FileName;
                try
                {
                   // DataExport.Save(_readerFactory.Results, dialog.FileName);
                    MessageBox.Show("Data byla uložena!");
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Nejsou data k uložení!","Chyba!",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }
    }
}
