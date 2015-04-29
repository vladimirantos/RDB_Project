﻿using System;
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
            LogGrid grid = new LogGrid(Log.GetData());
            grid.AddValues();
            Vypis.Children.Add(grid.Grid);
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            Log.Insert("Devices", 10);
            Log.Select("Devices", new Dictionary<string, string>() { { "accuracy", "0.5" }, { "description", "AHOJ" } }, 50);
            MessageBox.Show("Uloženo");
        }
    }
}
