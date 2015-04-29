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

namespace RDB_Project.Logging
{
    class LogGrid:DataGrid
    {
        private List<string> columnNames;
        private DataGrid grid;
        public LogGrid(List<string> columnNames)
        {
            this.columnNames = columnNames;
            this.grid = new DataGrid();
            this.SetColumnNames();
        }

        private void SetColumnNames()
        {
            foreach(string name in columnNames)
            {
                DataGridTextColumn c = new DataGridTextColumn();
                c.Header = name;
                c.Binding = new Binding(name);
                grid.Columns.Add(c);
            }
        }

        public void AddValues(List<Log> data)
        { 
            
        }
    }
}
