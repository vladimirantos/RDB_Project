using System;
using System.Collections.Generic;
using System.Globalization;
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
    //todo - vyřešit ukládání času do databáze

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
        public MainWindow()
        {
            InitializeComponent();
            //new NumberFormatInfo() { NumberDecimalSeparator = ".", NumberDecimalDigits = 3 },
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
            SearchResult argumentsResult = new SearchResult();
            argumentsResult.dateFrom = dateFrom.SelectedDate.Value;
            argumentsResult.dateTo = dateTo.SelectedDate.Value;
            argumentsResult.x = double.Parse(placeX.Text);
            argumentsResult.y = double.Parse(placeY.Text);
            argumentsResult.difference = double.Parse(variance.Text);
            argumentsResult.serialNumber = device.Text;

            DatabaseReader reader = new DatabaseReader(argumentsResult);

            MainGrid.Children.Add(element: View.SearchGrid.CreateGrid(reader.Search()));
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
    }
}
