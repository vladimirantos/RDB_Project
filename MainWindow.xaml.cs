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

namespace RDB_Project
{
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

        }

        private void _Log(object sender, RoutedEventArgs e)
        {

        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stop = new Stopwatch();
            DatabaseWriter.DeleteDatabase();
            stop.Start();
            DataWriteFactory.Create("data.csv", 1000000).Save();
            stop.Stop();
            MessageBox.Show(stop.Elapsed.ToString());
        }
    }
}
