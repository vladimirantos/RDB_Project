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
using EntityFramework.BulkInsert.Extensions;

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
            AddDialog dialog = new AddDialog();
            dialog.Show();
        }

        private void _Log(object sender, RoutedEventArgs e)
        {

        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stop = new Stopwatch();
            DatabaseWriter.DeleteDatabase();
            stop.Start();

            ///////////////////////////////////////////////////////
            // --- Entity test
            using (var ctx = new HovnoEntities())
            {
                data1 data = new data1();
                data.value1 = 1;
                List<data1> da = new List<data1>() { data };

                ctx.BulkInsert(da);
            }
            ///////////////////////////////////////////////////////

           /* DataWriteFactory.Create("data.csv", 1000000).Save();*/
            stop.Stop();
            MessageBox.Show(stop.Elapsed.ToString());
        }
    }
}
