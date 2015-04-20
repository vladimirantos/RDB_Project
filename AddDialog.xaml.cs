using Microsoft.Win32;
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

namespace RDB_Project
{
    /// <summary>
    /// Interaction logic for AddDialog.xaml
    /// </summary>
    public partial class AddDialog : Window
    {
        string _fileName;
        public AddDialog()
        {
            InitializeComponent();
            tl_upload.Visibility = System.Windows.Visibility.Hidden;
        }

        private void _OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CSV File (*.csv)|*.csv";

            Nullable<bool> result = dialog.ShowDialog();
                      
            if (result == true)
            {
                int size = 44;
                string name = dialog.FileName;
                if( name.Length > size)
                    name = name.Substring(0,size-1) + "...";

                this.text.Content = name;
                this._fileName = dialog.FileName;

                tl_upload.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void _Upload(object sender, RoutedEventArgs e)
        {
            // zpracovani souboru
        }
    }
}
