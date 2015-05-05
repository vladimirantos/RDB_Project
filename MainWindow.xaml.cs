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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using RDB_Project.DataWriting;
using RDB_Project.Logging;
using EntityFramework.BulkInsert.Extensions;

namespace RDB_Project
{
    //todo - vyřešit vlákna, aby nedocházelo k zatuhnutí okna při výpisu logu atd.
    //todo - centrování textu tlačítek - windows > 7 jsou vlevo
    //todo - vyřešit ukládání času do databáze

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
           /* try
            {
                LogGrid.Children.Add(element: Logging.SearchGrid.CreateGrid());
            }
            catch (RdbException mes)
            {
                MessageBox.Show(mes.Message, "Chyba!", MessageBoxButton.OK, MessageBoxImage.Information);
            }*/

            /*Log.Insert("Devices");
            MessageBox.Show("Uloženo");*/
        }
    }
}
