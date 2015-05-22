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
                LogGrid.Children.Add(element: Logging.LogGrid.CreateGrid(Log.GetData()));
            }
            catch (RdbException mes)
            {
                MessageBox.Show(mes.Message,"Chyba!",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            Paginator p = new Paginator(10, 108);
            p.CurrentPage = 11;
                MessageBox.Show(string.Format("Záznamy od {0} do {1}\nCelkem stran: {2}", p.Offset, p.Length,
                    p.TotalPages));
            /*Log.Insert("Devices");
            MessageBox.Show("Uloženo");*/
        }

        private void test_Click(object sender, RoutedEventArgs e)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(1432126015);
            MessageBox.Show(dateTime.ToString());
        }
    }
}
