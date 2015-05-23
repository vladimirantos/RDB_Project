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
        private int _itemsPerPage = 50;
        private Stopwatch timer = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
            buttonNext.Visibility = Visibility.Hidden;
            buttonBack.Visibility = Visibility.Hidden;
            MessageBlock.Text = "";
            TimeBlock.Text = "";
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

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            /*Paginator p = new Paginator(10, 108);
            p.CurrentPage = 11;
                MessageBox.Show(string.Format("Záznamy od {0} do {1}\nCelkem stran: {2}", p.Offset, p.Length,
                    p.TotalPages));*/
            /*Log.Insert("Devices");
            MessageBox.Show("Uloženo");*/
            /////////////////////////////////////////////////////////////
           
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

            _readerFactory = ReaderFactory.CreateFactory(argumentsResult, _itemsPerPage);


            try
            {
                timer.Start();
                StatusProgress.IsIndeterminate = true;
                UpdateGrid();
                timer.Stop();
                
            }
            catch (RdbException v)
            {
                MainGrid.Children.Clear();
                MessageBox.Show(v.Message);
            }
        }
        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            List<MType> mTypes = new List<MType>();
            MType mType = new MType();
            mType.idType = 1;
            mType.name = "A";
            mTypes.Add(mType);

            MType mType2 = new MType();
            mType2.idType = 1;
            mType2.name = "A";

            MessageBox.Show(mTypes.Contains(mType2).ToString());
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
            MainGrid.Children.Add(element: View.SearchGrid.CreateGrid(_readerFactory.GetResults().ToList()));

            MessageBlock.Text = "Výpis databáze";
            TimeBlock.Text = "Čas dotazu: "+timer.Elapsed.ToString();
            timer.Reset();
            // StatusProgress.IsIndeterminate = false;
            //StatusProgress.Value = 100;
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
    }
}
