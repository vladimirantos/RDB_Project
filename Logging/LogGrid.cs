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
using System.Data;
using RDB_Project.DataWriting;
using RDB_Project.Logging;
using EntityFramework.BulkInsert.Extensions;
using System.Reflection;

namespace RDB_Project.Logging
{
    class LogGrid:DataGrid
    {
        private List<Dblog> data;
        private DataGrid grid;
        public LogGrid(List<Dblog> data)
        {
            this.data = data;
            this.grid = new DataGrid();
            this.SetColumnNames();
        }

        private void SetColumnNames()
        {
           PropertyInfo[] names =  data[0].GetType().GetProperties();

           foreach (PropertyInfo prp in names)
            {
                string name = prp.Name;
                if(name != "id_log")
                { 
                    DataGridTextColumn c = new DataGridTextColumn();
                    c.Header = name;
                    c.Binding = new Binding(name);
                    grid.Columns.Add(c);
                }
            }
        }

        public void AddValues()
        {
            grid.AutoGenerateColumns = true;
            foreach (Dblog item in data)
            {
                grid.Items.Add(item);
            }
        }

        public DataGrid Grid
        {
            get { return grid; }
        }
    }
}
